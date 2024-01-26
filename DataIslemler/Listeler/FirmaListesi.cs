using DataIslemler.Data;
using System.Collections.Generic;
using System.Linq;

namespace DataIslemler.Listeler
{
    public class FirmaListesi
    {
        Musavir_DbDataContext data;

        public FirmaListesi(string dataAdi)
        {
            data = new Musavir_DbDataContext(new Helpers.Baglanti(dataAdi).ConStr);
        }

        public List<Models.Firma> GetFirmaListesi()
        {

            var result = (from m in data.FIRMA_TANIMLARIs /*where m.GIB_KODU.Length>0 && m.GIB_PAROLA.Length>0 && m.GIB_SIFRE.Length>0*/
                          select new Models.Firma
                          {

                              Id = m.ID,
                              Unvan = m.UNVAN,
                              GibKodu = m.GIB_KODU,
                              GibParola = m.GIB_PAROLA,
                              GibSifre = m.GIB_SIFRE
                          }).ToList();

            return result;
        }
    }
}
