using DataIslemler.Data;
using DataIslemler.Helpers;
using System.Collections.Generic;
using System.Linq;
using Tacmin.Model.Mukellef;

namespace DataIslemler.Listeler.Mukellef
{
    public class BeyannameDonemListesi
    {
        Musavir_DbDataContext data;

        public BeyannameDonemListesi(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            
        }

        public List<BeyannameDonemModel> DonemListesi()
        {

            var liste = (from m in data.Beyanname_Donemlers
                         select new BeyannameDonemModel
                         {
                             Id = m.Id,
                             Donem_Adi = m.Donem
                         }).ToList();

            return liste;
        }
    }
}
