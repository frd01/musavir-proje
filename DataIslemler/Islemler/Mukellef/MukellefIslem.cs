using DataIslemler.Data;
using DataIslemler.Helpers;
using System.Linq;

namespace DataIslemler.Islemler.Mukellef
{
    public class MukellefIslem
    {
        Musavir_DbDataContext data;

        public MukellefIslem(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public int? MukellefKontrol(string vergi_no)
        {
            try
            {
                var model = data.FIRMA_TANIMLARIs.Where(x => x.VERGI_NO == vergi_no).SingleOrDefault();
                if (model == null)
                    return null;
                return model.ID;
            }
            catch
            {

                return null;
            }
        }
    }
}
