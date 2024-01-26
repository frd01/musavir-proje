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

namespace Tacmin.Service.Sorgulamalar.Tebligat
{
    public class TebligatIslem
    {
        private ServiceIslem service;
        private Girisler.InteraktifVd vdIslem;
        private Firma firma;
        private DataIslemler.Tebligat.GelirIdaresi.ListeIslem listeIslem;

        private string servisUrl = "https://ivd.gib.gov.tr/tvd_server/dispatch";


        public TebligatIslem(Firma _firma,string dataAdi)
        {
            service = new ServiceIslem();
            vdIslem = new Girisler.InteraktifVd(service);
            firma = _firma;

            listeIslem = new DataIslemler.Tebligat.GelirIdaresi.ListeIslem(dataAdi);
        }
        public (List<GelirMlyResponseModel>,List<GibTabloSonuc>) TebligatSorgula()
        {

            var token_islem = vdIslem.GirisIslem(firma.GibKodu, firma.GibSifre, firma.GibParola);

            var hata_listesi = new List<GibTabloSonuc>();
            var teb_list = new List<GelirMlyResponseModel>();

            if (token_islem.messages != null)
            {
                if (token_islem.messages.Count >0)
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
                var data_str = "cmd=mukellefService_zarflariGetir&callid=4df914e8fb608-33&pageName=undefined&token=" + token_islem.token
     + "&jp=%7B%22parametreler%22%3A%7B%22duzBirim%22%3A%22%22%2C%22duzBasZaman%22%3A%22%22%2C%22duzBitZaman%22%3A%22%22%2C%22tebligBasZaman%22%3A%22%22%2C%22tebligBitZaman%22%3A%22%22%2C%22durum%22%3A%5B%22400%22%2C%22500%22%2C%22600%22%5D%2C%22belgeTuru%22%3A%22%22%2C%22belgeNo%22%3A%22%22%2C%22paging%22%3Atrue%7D%2C%22respKeyParam%22%3A%22list%22%2C%22pv%22%3A%7B%22start%22%3A0%2C%22limit%22%3A%2225%22%2C%22sorters%22%3A%5B%5D%7D%7D";


                var response = service.PostJsonRequest(servisUrl, data_str);

                var tebligat_listesi = GetList(response);

                if (tebligat_listesi.Count > 0)
                {
                    tebligat_listesi = listeIslem.ListeKayitKontrolIslem(tebligat_listesi);

                    if (tebligat_listesi.Count > 0)
                    {
                        foreach (var item in tebligat_listesi)
                        {
                            item.ustYaziIcerik = GetUstYaziIcerik(item.belgeOid,token_islem.token);
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

        public List<GelirMlyResponseModel> GetList(string response)
        {
            var tebligat_listesi = new List<GelirMlyResponseModel>();
            try
            {
                var result = JsonConvert.DeserializeObject<GelirMlyResponseModel_Data_1>(response);

                tebligat_listesi = result.data.list;
            }
            catch
            {

                tebligat_listesi = new List<GelirMlyResponseModel>();
            }

            return tebligat_listesi;
        }

        private byte[] GetUstYaziIcerik(string belgeOid,string token)
        {

            var dosya_link = "https://ivd.gib.gov.tr/tvd_server/islem/pdf/?oid=" + belgeOid
                   + "&dosyaismi=" + belgeOid
                   + "&uzanti=imz&tur=teblig&belgeTuru=tebligat&cmd=getTebligatPdf&islem=view&token=" + token
                   + "&userId=26105500";

            var file = GetFile(dosya_link);

            return file;
        }

        private byte[] GetFile(string url)
        {
            var file = service.GetFileByte(url).Result;

            return file;
        }
    }
}
