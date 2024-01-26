using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model.Mukellef;

namespace DataIslemler.Main
{
    public class ListeIslem
    {
        private Musavir_DbDataContext data;

        public ListeIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public List<FirmaGibModel> GetGibFirmaListesi()
        {

            var liste = new List<FirmaGibModel>();

            foreach (var item in data.GetFirmaOtoSorguList())
            {
                var model = new FirmaGibModel();
                model.Id = item.ID;
                model.Unvan = item.UNVAN;
                model.GibKodu = item.GIB_KODU;
                model.GibParola = item.GIB_PAROLA;
                model.GibSifre = item.GIB_SIFRE;

                liste.Add(model);
            }

            return liste;
        }

        public bool GunlukOtoSorguDurum()
        {
            var kontrol = data.SorguTakips.Where(x => x.OtomatikIndirme == DateTime.Now).ToList();

            if (kontrol.Count > 0)
                return true;

            return false;
        }
    }
}
