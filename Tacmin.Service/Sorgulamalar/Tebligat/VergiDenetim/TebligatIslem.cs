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

namespace Tacmin.Service.Sorgulamalar.Tebligat.VergiDenetim
{
    public class TebligatIslem
    {
        private ServiceIslem service;
        private Girisler.InteraktifVd vdIslem;
        private Firma firma;
        private DataIslemler.Tebligat.VergiDenetim.ListeIslem listeIslem;

        private string servisUrl = "https://ivd.gib.gov.tr/tvd_server/dispatch";


        public TebligatIslem(Firma _firma, string dataAdi)
        {
            service = new ServiceIslem();
            vdIslem = new Girisler.InteraktifVd(service);
            firma = _firma;

            listeIslem = new DataIslemler.Tebligat.VergiDenetim.ListeIslem(dataAdi);
        }

        public (List<VergiDenetimMlyListeModel>,List<GibTabloSonuc>) TebligatSorgula()
        {

            var token_islem = vdIslem.GirisIslem(firma.GibKodu, firma.GibSifre, firma.GibParola);

            var hata_listesi = new List<GibTabloSonuc>();
            var teb_list = new List<VergiDenetimMlyListeModel>();

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
                var data_str = "cmd=mukellefService_tebligatlariListele&callid=9747f1ac9a99c-31&pageName=RG_DIGER_TEBLIGAT&token=" + token_islem.token
       + "&jp=%7B%22kurumKodu%22%3A%2246622688%22%2C%22ilkListelemeMi%22%3Atrue%7D";

                var response = service.PostJsonRequest(servisUrl, data_str);

                var tebligat_listesi = GetList(response);

                if (tebligat_listesi.Count > 0)
                {
                    tebligat_listesi = listeIslem.MaliyeListeKayitKontrolIslem(tebligat_listesi);

                    if (tebligat_listesi.Count > 0)
                    {
                        foreach (var item in tebligat_listesi)
                        {
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

        private List<VergiDenetimMlyListeModel> GetList(string response)
        {
            var tebligat_listesi = new List<VergiDenetimMlyListeModel>();

            try
            {
                var result = JsonConvert.DeserializeObject<VergiDenetimMlyListeModel_Data_1>(response);

                tebligat_listesi = result.data.data;
            }
            catch
            {

                tebligat_listesi = new List<VergiDenetimMlyListeModel>();
            }

            return tebligat_listesi;
        }

        private List<VergiDenetimMDosyaModel> EkIslem(string oid,string token)
        {
            var liste = new List<VergiDenetimMDosyaModel>();

            var data_str = "cmd=mukellefService_ketsisTebligEkListele&callid=b354bb33bd396-33&pageName=RG_DIGER_TEBLIGAT&token=" + token
        + "&jp=%7B%22kurumKodu%22%3A%2246622688%22%2C%22tebligOid%22%3A%22"
        + oid + "%22%7D";

            var res_ek = service.PostJsonRequest(servisUrl, data_str);

            var result = JsonConvert.DeserializeObject<TicaretEkResModel_Data>(res_ek);

            var mly_ek_list = result.data;

            if (mly_ek_list.Count > 0)
            {
                foreach (var item in mly_ek_list)
                {
                    var ek_link = "https://ivd.gib.gov.tr/tvd_server/tebligat/pdf/?cmd=getKetsisPdf&tur=view&=&oid=" + item.oid
               + "&kurumKodu=46622688&bucketTuru=TEBLIG&islemTuru=ek&token="
               + token;

                    var model = new VergiDenetimMDosyaModel();

                    model.icerik = GetFile(ek_link);


                    liste.Add(model);
                }
            }

            return liste;
        }

        private byte[] GetUstYaziIcerik(string oid,string token)
        {

            var yazi_link = "https://ivd.gib.gov.tr/tvd_server/tebligat/pdf/?cmd=getKetsisPdf&tur=view&=&oid=" + oid
        + "&kurumKodu=46622688&bucketTuru=TEBLIG&islemTuru=teblig&token="
        + token;

            var file = GetFile(yazi_link);

            return file;
        }

        private byte[] GetFile(string url)
        {
            var file = service.GetFileByte(url).Result;

            return file;
        }
    }
}
