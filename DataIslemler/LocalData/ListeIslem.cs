using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model.LocalData;

namespace DataIslemler.LocalData
{
    public class ListeIslem
    {
        private Musavir_DbDataContext data;

        public ListeIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public LocalDataModel LocalListeler()
        {
            var model = new LocalDataModel();

            model.FirmaListesi = GetFirmaListesi();

            return model;
        }

        private List<FirmaModel> GetFirmaListesi()
        {

            var liste = new List<FirmaModel>();

            var firma_listesi = data.FIRMA_TANIMLARIs.Where(x => x.AKTIF == true).ToList();

            foreach (var item in firma_listesi)
            {
                var model = new FirmaModel();

                model.Id = item.ID;
                model.Unvan = item.UNVAN;
                model.GibKodu = item.GIB_KODU;
                model.GibParola = item.GIB_PAROLA;
                model.GibSifre = item.GIB_SIFRE;

                liste.Add(model);
            }

            return liste;
        }
    }
}
