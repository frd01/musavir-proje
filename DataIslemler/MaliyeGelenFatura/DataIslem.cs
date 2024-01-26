using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model;
using Tacmin.Model.Maliye;

namespace DataIslemler.MaliyeGelenFatura
{
    public class DataIslem
    {
        private Musavir_DbDataContext data;

        private List<Maliye_Gelen_Faturalar> data_fatura_listesi;

        public DataIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            data_fatura_listesi = data.Maliye_Gelen_Faturalars.ToList();
        }

        public KayitResModel FaturaKayit(List<GelenFaturaModel> clientList,int? firmaId)
        {
            var res_model = new KayitResModel();

            res_model.IslemDurum = true;

            try
            {
                foreach (var item in clientList)
                {
                    if (faturaKayitKontrol(item.faturaNo, item.unvan) <= 0)
                    {
                        var model = new Maliye_Gelen_Faturalar();

                        model.FaturaNo = item.faturaNo;
                        model.GonderimSekli = item.gonderimSekli;
                        model.Odenecek = item.odenecek;
                        model.ParaBirimi = item.paraBirimi;
                        model.Tarih = Convert.ToDateTime(item.tarih);
                        model.TesisatNo = item.tesisatNo;
                        model.Toplam = item.toplam;
                        model.Unvan = item.unvan;
                        model.Vergi = item.vergi;
                        model.VergiNo = item.mukVkn;
                        model.FirmaId = firmaId;

                        data.Maliye_Gelen_Faturalars.InsertOnSubmit(model);
                        data.SubmitChanges();
                    }
                }

                res_model.IslemDurum = true;
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message;
            }

            return res_model;
        }

        public List<GelenFaturaModel> FaturOtoKayit(List<GelenFaturaModel> clientList)
        {
            var liste = new List<GelenFaturaModel>(); 

            foreach (var item in clientList)
            {
                if (faturaKayitKontrol(item.faturaNo,item.unvan) <=0)
                {
                    var model = new Maliye_Gelen_Faturalar();

                    model.FaturaNo = item.faturaNo;
                    model.GonderimSekli = item.gonderimSekli;
                    model.Odenecek = item.odenecek;
                    model.ParaBirimi = item.paraBirimi;
                    model.Tarih = Convert.ToDateTime(item.tarih);
                    model.TesisatNo = item.tesisatNo;
                    model.Toplam = item.toplam;
                    model.Unvan = item.unvan;
                    model.Vergi = item.vergi;
                    model.VergiNo = item.mukVkn;
                    model.FirmaId = item.firmaId;

                    data.Maliye_Gelen_Faturalars.InsertOnSubmit(model);
                    data.SubmitChanges();

                    liste.Add(item);

                }
            }

            return liste;
        }

        private int faturaKayitKontrol(string faturaNo, string unvan)
        {
            var kontrol = data_fatura_listesi.Where(x => x.FaturaNo == faturaNo && x.Unvan == unvan).ToList();

            return kontrol.Count;
        }
    }
}
