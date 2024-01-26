using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model.KontrolIslem;

namespace DataIslemler.KontrolIslem
{
    public class ListeIslem
    {
        private Musavir_DbDataContext data;

        public ListeIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public List<FirmaKontrolModel> FirmaKontrolListesi()
        {
            var liste = new List<FirmaKontrolModel>();

            var data_list = data.FIRMA_TANIMLARIs.Where(x => x.AKTIF == true).ToList();

            foreach (var item in data_list)
            {
                if (!string.IsNullOrEmpty(item.GIB_KODU) && !string.IsNullOrEmpty(item.GIB_PAROLA)
                    && !string.IsNullOrEmpty(item.GIB_SIFRE)
                    )
                {
                    var model = new FirmaKontrolModel();

                    model.Id = item.ID;
                    model.Unvan = item.UNVAN;
                    model.GibKodu = item.GIB_KODU;
                    model.GibParola = item.GIB_PAROLA;
                    model.GibSifre = item.GIB_SIFRE;
                    model.IslemDurum = false;

                    liste.Add(model);
                }
            }

            return liste;

        }
    }
}
