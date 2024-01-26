using System.Collections.Generic;
using System.Linq;

namespace DataIslemler.Listeler.Gib
{
    public class GelenFaturaListesi
    {
        Data.Musavir_DbDataContext data;

        public GelenFaturaListesi(string dataAdi)
        {
            var con_str = new Helpers.Baglanti(dataAdi).ConStr;
            data = new Data.Musavir_DbDataContext(con_str);
        }

        public List<Tacmin.Data.DbModel.GELEN_FATURALAR> FaturaListesi()
        {

            var liste = new List<Tacmin.Data.DbModel.GELEN_FATURALAR>();

            foreach (var item in data.GELEN_FATURALARs.ToList())
            {
                var model = new Tacmin.Data.DbModel.GELEN_FATURALAR();
                model.ID = item.ID;
                model.FATURA_NO = item.FATURA_NO;
                model.FIRMA_ID = item.FIRMA_ID.Value;
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

                liste.Add(model);

            }

            return liste;
        }
    }
}
