using DataIslemler.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Model.Gib;
using Tacmin.Model.Tebligat;

namespace Tacmin.Service.Sorgulamalar.Tebligat.TicaretBakanligi
{
    public class TebligatIslem
    {
        private ServiceIslem service;
        private Girisler.InteraktifVd vdIslem;
        private Firma firma;
        private DataIslemler.Tebligat.TicaretBakanligi.ListeIslem listeIslem;

        private string servisUrl = "https://ivd.gib.gov.tr/tvd_server/dispatch";


        public TebligatIslem(Firma _firma, string dataAdi)
        {
            service = new ServiceIslem();
            vdIslem = new Girisler.InteraktifVd(service);
            firma = _firma;

            listeIslem = new DataIslemler.Tebligat.TicaretBakanligi.ListeIslem(dataAdi);
        }

        public (List<TicaretMlyResponseModel>,List<GibTabloSonuc>) TebligatSorgula()
        {
            var token_islem = vdIslem.GirisIslem(firma.GibKodu, firma.GibSifre, firma.GibParola);

            var hata_listesi = new List<GibTabloSonuc>();
            var teb_list = new List<TicaretMlyResponseModel>();

            if (token_islem.messages != null)
            {
                if (token_islem.messages.Count > 0)
                {
                    var hata_model = new GibTabloSonuc();
                    hata_model.Mesaj = token_islem.messages[0].text;
                    hata_model.Unvan = firma.Unvan;
                    hata_model.Id = firma.Id;

                    hata_listesi.Add(hata_model);
                }
            }
            try
            {
                var data_str = "cmd=mukellefService_tebligatlariListele&callid=112556a2970d8-31&pageName=RG_DIGER_TEBLIGAT&token=" + token_islem.token
        + "&jp=%7B%22kurumKodu%22%3A%2224308261%22%2C%22ilkListelemeMi%22%3Atrue%7D";

                var response = service.PostJsonRequest(servisUrl, data_str);

                var tebligat_listesi = GetList(response);

                if (tebligat_listesi.Count > 0)
                {
                    tebligat_listesi = listeIslem.MaliteListeKayitKontrolIslem(tebligat_listesi);

                    if (tebligat_listesi.Count > 0)
                    {
                        foreach (var item in tebligat_listesi)
                        {
                            Console.WriteLine(item.oid);
                            item.ekListesi = EkIslem(item.oid, token_islem.token);
                            item.ustYaziIcerik = GetUstYaziIcerik(item.oid, token_islem.token);

                            teb_list.Add(item);
                        }
                    }
                }

                vdIslem.CikisIslem(token_islem.token);
            }
            catch (Exception)
            {

                vdIslem.CikisIslem(token_islem.token);
            }

            return (teb_list, hata_listesi);
        }

        public List<TicaretMlyResponseModel> GetList(string response)
        {
            var tebligat_listesi = new List<TicaretMlyResponseModel>();

            try
            {
                var result = JsonConvert.DeserializeObject<TicaretMlyResponseModel_Data_1>(response);

                tebligat_listesi = result.data.data;
            }
            catch
            {

                tebligat_listesi = new List<TicaretMlyResponseModel>();
            }

            return tebligat_listesi;
        }

        public List<TicaretMlyDosyaModel> EkIslem(string oid,string token)
        {

            var liste = new List<TicaretMlyDosyaModel>();

            var data_str = "cmd=mukellefService_ketsisTebligEkListele&callid=112556a2970d8-33&pageName=RG_DIGER_TEBLIGAT&token=" + token
       + "&jp=%7B%22kurumKodu%22%3A%2214467936%22%2C%22tebligOid%22%3A%22" + oid
       + "%22%7D";

            var response = service.PostJsonRequest(servisUrl, data_str);

            var result = JsonConvert.DeserializeObject<TicaretEkResModel_Data>(response);

            var ek_listesi = result.data;

            foreach (var item in ek_listesi)
            {
                var ek_link = "https://ivd.gib.gov.tr/tvd_server/tebligat?token=" + token
                + "&cmd=downloadKetsisPdf&kurumKodu=14467936&oid=" + item.oid
                + "&islemTuru=ek&tur=download&bucketTuru=TEBLIG&dosyaAdi=" + item.oid + ".pdf&";

                var model = new TicaretMlyDosyaModel();
                model.gonderilenDosyaAdi = item.gonderilenDosyaAdi;
                model.icerik = GetFile(ek_link);

                liste.Add(model);
            }

            return liste;
        }

        private byte[] GetUstYaziIcerik(string oid,string token)
        {
            var ust_yazi_link = "https://ivd.gib.gov.tr/tvd_server/tebligat/pdf/?cmd=getKetsisPdf&tur=view&=&oid=" + oid
       + "&kurumKodu=14467936&bucketTuru=TEBLIG&islemTuru=teblig&token=" + token;

            var file = GetFile(ust_yazi_link);

            return file;
        }


        private byte[] GetFile(string url)
        {
            var file = service.GetFileByte(url).Result;

            return file;
        }
    }
}
