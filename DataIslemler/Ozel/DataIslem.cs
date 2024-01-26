using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Helpers;
     

namespace DataIslemler.Ozel
{
    public class DataIslem
    {
        private DataIslemler.AdminData.Yonetim_DbDataContext adminData;

        public DataIslem(string dataAdi)
        {
            var ad_str = new Baglanti(dataAdi).AdmConStr;

            adminData = new AdminData.Yonetim_DbDataContext(ad_str);
        }

        public void KullaniciGirisKaydi(int userId)
        {
            var tarih = DateTime.Now;
            //tarih = Convert.ToDateTime(tarih.ToString("dd.MM.yyyy"));

            var liste = adminData.KullaniciGirisleris.Where(x => x.UserId == userId && x.Tarih == tarih).ToList();

            if (liste.Count<=0)
            {
                var model = new DataIslemler.AdminData.KullaniciGirisleri();

                model.UserId = userId;
                model.Tarih = tarih;

                adminData.KullaniciGirisleris.InsertOnSubmit(model);
                adminData.SubmitChanges();
            }
        }
    }
}
