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

namespace Tacmin.Service.Sorgulamalar.Tebligat.İcisleriBakanligi
{
    public class TebligatIslem
    {
        private ServiceIslem service;
        private Girisler.InteraktifVd vdIslem;
        private Firma firma;
        private DataIslemler.Tebligat.Icisleri.ListeIslem listeIslem;

        private string servisUrl = "https://ivd.gib.gov.tr/tvd_server/dispatch";


        public TebligatIslem(Firma _firma, string dataAdi)
        {
            service = new ServiceIslem();
            vdIslem = new Girisler.InteraktifVd(service);
            firma = _firma;

            listeIslem = new DataIslemler.Tebligat.Icisleri.ListeIslem(dataAdi);
        }

        public (List<IsisleriResponseListe>,List<GibTabloSonuc>) TebligatSorgula()
        {

            var token_islem = vdIslem.GirisIslem(firma.GibKodu, firma.GibSifre, firma.GibParola);

            var teb_list = new List<IsisleriResponseListe>();
            var hata_listesi = new List<GibTabloSonuc>();

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
                var data_str = "cmd=mukellefService_tebligatlariListele&callid=6fcb0e888718f-31&pageName=RG_DIGER_TEBLIGAT&token=" + token_islem.token
        + "&jp=%7B%22kurumKodu%22%3A%2224312041%22%2C%22ilkListelemeMi%22%3Atrue%7D";

                var response = service.PostJsonRequest(servisUrl, data_str);

                var tebligat_listesi = GetList(response);

                if (tebligat_listesi.Count > 0)
                {
                    tebligat_listesi = listeIslem.MaliyeListeKayitKontrolIslem(tebligat_listesi);

                    if (tebligat_listesi.Count > 0)
                    {
                        foreach (var item in tebligat_listesi)
                        {
                            item.ustYaziIcerik = GetUsYaziIcerik(item.kurumKodu, item.oid, token_islem.token);

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

        private List<IsisleriResponseListe> GetList(string response)
        {
            var tebligat_listesi = new List<IsisleriResponseListe>();

            try
            {
                var result = JsonConvert.DeserializeObject<IsisleriResponseListe_Data_1>(response);

                tebligat_listesi = result.data.data;
            }
            catch
            {

                tebligat_listesi = new List<IsisleriResponseListe>();
            }

            return tebligat_listesi;
        }

        private byte[] GetUsYaziIcerik(string kurumKodu, string oid,string token)
        {
            var ust_yazi_link = "https://ivd.gib.gov.tr/tvd_server/tebligat?token=" + token
        + "&cmd=downloadKetsisPdf&kurumKodu=" + kurumKodu
        + "&oid=" + oid
        + "&islemTuru=teblig&tur=download&bucketTuru=TEBLIG&dosyaAdi=" + oid + ".pdf&";

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
