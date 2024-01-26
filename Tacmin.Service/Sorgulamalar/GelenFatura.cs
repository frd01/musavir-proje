using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Model;
using Tacmin.Model.LocalData;
using Tacmin.Model.Maliye;

namespace Tacmin.Service.Sorgulamalar
{
    public class GelenFatura
    {
        private string dataAdi;
        private FirmaModel firma;
        private DateTime? ilkTarih;
        private DateTime? sonTarih;
        private DataIslemler.MaliyeGelenFatura.DataIslem dataIslem;

        private ServiceIslem service;

        private string servisUrl = "https://ivd.gib.gov.tr/tvd_server/dispatch";

        public GelenFatura(string _dataAdi,FirmaModel _firma,DateTime? _ilkTarih,DateTime? _sonTarih)
        {
            dataAdi = _dataAdi;
            firma = _firma;
            ilkTarih = _ilkTarih;
            sonTarih = _sonTarih;

            dataIslem = new DataIslemler.MaliyeGelenFatura.DataIslem(dataAdi);
        }

        public KayitResModel SorguIslem()
        {

            var res_model = new KayitResModel();

            res_model.IslemDurum = true;

            service = new ServiceIslem();

            var giris_islem = new Girisler.InteraktifVd(service);

            var token_islem = giris_islem.GirisIslem(firma.GibKodu, firma.GibSifre, firma.GibParola);

            try
            {
                var tarih_listesi = TarihAralikListesi(ilkTarih, sonTarih);

               

                if (token_islem.messages != null)
                {
                    if (token_islem.messages.Count > 0)
                    {
                        res_model.IslemDurum = false;
                        res_model.Mesaj = token_islem.messages[0].text;
                        return res_model;
                    }
                }

                var fatura_listesi = new List<GelenFaturaModel>();

                foreach (var item in tarih_listesi)
                {
                    var result = GetFaturaListesi(item.BaslangicTarih, item.BitisTarih, token_islem.token);

                    foreach (var fatura in result)
                    {
                        fatura_listesi.Add(fatura);
                    }
                }

                if (fatura_listesi.Count > 0)
                {
                    dataIslem.FaturaKayit(fatura_listesi, firma.Id);
                }

                res_model.IslemDurum = true;

                giris_islem.CikisIslem(token_islem.token);
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message;
                giris_islem.CikisIslem(token_islem.token);
            }

            return res_model;
        }

        private List<GelenFaturaModel> GetFaturaListesi(string ilkTarih, string sonTarih,string token)
        {
            var data_str = "cmd=EFaturaIslemleri_eFaturaGoruntuleSorgula&callid=074d4fa89e435-25&pageName=P_EFATURA&token=" + token
       + "&jp=%7B%22faturaTarihBas%22%3A%22" + ilkTarih
       + "%22%2C%22textBox%22%3A%22%22%2C%22faturaTarihSon%22%3A%22" + sonTarih
       + "%22%7D";

            var response = service.PostJsonRequest(servisUrl, data_str);

            var fatura_listesi = FaturaKontrol(response);



            return fatura_listesi;
        }

        private List<GelenFaturaModel> FaturaKontrol(string response)
        {
            var liste = new List<GelenFaturaModel>();

            try
            {
                var result = JsonConvert.DeserializeObject<GelenFaturaModel_Data_1>(response);
                liste = result.data.FATURASONUC;
            }
            catch (Exception)
            {

                liste = new List<GelenFaturaModel>();
            }

            return liste;
        }

        private List<TarihListeModel> TarihAralikListesi(DateTime? _ilkTarih, DateTime? _sonTarih)
        {

            var liste = new List<TarihListeModel>();

            var ilkTarih = _ilkTarih.Value;
            var sonTarih = _sonTarih.Value;

            while (ilkTarih < sonTarih)
            {
                var model = new TarihListeModel();
                model.BaslangicTarih = ilkTarih.ToString("yyyy-MM-dd");
                var tarih = ilkTarih.AddDays(7);
                if (tarih > sonTarih)
                {
                    model.BitisTarih = sonTarih.ToString("yyyy-MM-dd");
                    liste.Add(model);
                    break;
                }
                if (tarih < sonTarih)
                {
                    model.BitisTarih = tarih.ToString("yyyy-MM-dd");
                    liste.Add(model);
                    ilkTarih = tarih;
                }
                else
                {
                    model.BitisTarih = sonTarih.ToString("yyyy-MM-dd");
                    liste.Add(model);
                    break;
                }

                ilkTarih = ilkTarih.AddDays(1);
            }

            return liste;
        }
    }
}
