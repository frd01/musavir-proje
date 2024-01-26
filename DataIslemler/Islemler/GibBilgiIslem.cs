using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.AdminData;
using DataIslemler.Helpers;

namespace DataIslemler.Islemler
{
    public class GibBilgiIslem
    {
        private Yonetim_DbDataContext data;

        private string dataAdi;

        public GibBilgiIslem(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).AdmConStr;

            data = new Yonetim_DbDataContext(con_str);

            this.dataAdi = dataAdi;
        }

        public Tacmin.Model.KullaniciModel GetGibBilgi(string kullaniciKodu)
        {

            var model = new Tacmin.Model.KullaniciModel();

            var kontrol = data.KULLANICI_TANIMLARIs.Where(x=>x.KULLANICI_KODU == kullaniciKodu).Count();

            if (kontrol > 0)
            {
                var data_model = data.KULLANICI_TANIMLARIs.Where(x=>x.KULLANICI_KODU == kullaniciKodu).ToList()[0];

                model.Id = data_model.ID;
                model.GibKodu = data_model.GIB_KODU;
                model.GibSifre = data_model.GIB_SIFRE;
            }


            return model;

        }
    }
}
