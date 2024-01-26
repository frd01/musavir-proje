using System;
using System.Collections.Generic;
using System.Linq;

namespace DataIslemler.Islemler.Gib
{
    public class GelenFatura
    {
        Data.Musavir_DbDataContext data;

        private List<Data.GELEN_FATURALAR> data_fatura_listesi;

        public GelenFatura(string dataAdi)
        {
            var con_str = new Helpers.Baglanti(dataAdi).ConStr;
            data = new Data.Musavir_DbDataContext(con_str);

            data_fatura_listesi = data.GELEN_FATURALARs.ToList();
        }

        public void GelenFaturaKayit(List<Tacmin.Data.DbModel.GELEN_FATURALAR> fatura_listesi)
        {

            var data_liste = new List<Data.GELEN_FATURALAR>();

            foreach (var item in fatura_listesi)
            {
                var model = new Data.GELEN_FATURALAR();
                model.FATURA_NO = item.FATURA_NO;
                model.FIRMA_ID = item.FIRMA_ID;
                model.GONDERIM_SEKLI = item.GONDERIM_SEKLI;
                model.KDV_0 = item.KDV_0;
                model.KDV_1 = item.KDV_1;
                model.KDV_10 = item.KDV_10;
                model.KDV_20 = item.KDV_20;
                model.ODENECEK = item.ODENECEK;
                model.TARIH = item.TARIH;
                model.TESISAT_NO = item.TESISAT_NO;
                model.TOPLAM = item.TOPLAM;
                model.TOPLAM_0 = item.TOPLAM_0;
                model.TOPLAM_1 = item.TOPLAM_1;
                model.TOPLAM_10 = item.TOPLAM_10;
                model.TOPLAM_20 = item.TOPLAM_20;
                model.UNVAN = item.UNVAN;
                model.VERGI = item.VERGI;
                model.VERGI_NO = item.VERGI_NO;

                if (fatura_kontrol(model.FATURA_NO))
                    data_liste.Add(model);


            }

            data.GELEN_FATURALARs.InsertAllOnSubmit(data_liste);
            data.SubmitChanges();
        }

        private bool fatura_kontrol(string fatura_no)
        {
            try
            {
                var durum = true;
                foreach (var item in data_fatura_listesi)
                {
                    if (item.FATURA_NO == fatura_no)
                    {
                        durum = false;
                        break;
                    }
                }
                return durum;
            }
            catch (Exception)
            {

                return true;
            }
        }
    }
}
