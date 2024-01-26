using DataIslemler.Data;
using DataIslemler.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace DataIslemler.Listeler
{
    public class MaliyeListeler
    {
        Musavir_DbDataContext data;

        public MaliyeListeler(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public List<Tacmin.Model.MaliyeIslemleri.VergiTurModel> VergiTurListesi()
        {

            var liste = (from m in data.Maliye_Vergi_Turleris
                         select new Tacmin.Model.MaliyeIslemleri.VergiTurModel
                         {

                             Id = m.Id,
                             Beyanname_Kodu = m.Beyanname_Kodu,
                             Vergi_Kodu = m.Vergi_Kodu,
                             Beyanname_Adi = m.Beyanname_Adi
                         }).ToList();

            return liste;
        }

        public List<Tacmin.Model.MaliyeIslemleri.VergiDairesiModel> VergiDaireListesi()
        {

            var liste = (from m in data.Vergi_Daireleris
                         select new Tacmin.Model.MaliyeIslemleri.VergiDairesiModel
                         {
                             Id = m.Id,
                             Daire_Adi = m.Daire_Adi,
                             Kodu = m.Daire_Adi
                         }).ToList();

            return liste;
        }
    }
}
