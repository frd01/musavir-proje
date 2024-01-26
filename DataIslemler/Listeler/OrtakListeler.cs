using DataIslemler.Data;
using DataIslemler.Helpers;
using System.Collections.Generic;
using System.Linq;
using Tacmin.Model.Ortak;


namespace DataIslemler.Listeler
{
    public class OrtakListeler
    {
        Musavir_DbDataContext data;

        public OrtakListeler(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public List<SehirModel> SehirListesi()
        {

            var liste = (from m in data.Sehirlers
                         select new SehirModel
                         {
                             Id = m.Id,
                             Sehir_Adi = m.Sehir_Adi,
                             Plaka_Kodu = m.Plaka_Kodu
                         }).ToList();

            return liste;
        }

        public List<IlceModel> IlceListesi()
        {

            var liste = (from m in data.Ilcelers
                         select new IlceModel
                         {
                             Id = m.Id,
                             Sehir_Id = m.Sehir_Id,
                             Ilce_Adi = m.Ilce_Adi
                         }).ToList();

            return liste;
        }

        public List<VergiDaireModel> VergiDaireListesi()
        {

            var liste = (from m in data.Vergi_Daireleris
                         select new VergiDaireModel
                         {
                             Id = m.Id,
                             Daire_Adi = m.Daire_Adi,
                             Kodu = m.Kodu
                         }).ToList();

            return liste;
        }

        public List<IlceModel> Get_IlceListesi(int sehir_id)
        {

            var liste = (from m in data.Ilcelers
                         where m.Sehir_Id == sehir_id
                         select new IlceModel
                         {
                             Id = m.Id,
                             Sehir_Id = m.Sehir_Id,
                             Ilce_Adi = m.Ilce_Adi
                         }).ToList();

            return liste;
        }


    }
}
