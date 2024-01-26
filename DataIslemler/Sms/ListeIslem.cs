using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Helpers;
using Tacmin.Model;
using Tacmin.Model.Sms;
using Tacmin.Model.Whatsapp;

namespace DataIslemler.Sms
{
    public class ListeIslem
    {
        private string dataAdi;
        private DataIslemler.AdminData.Yonetim_DbDataContext adminData;

        public ListeIslem(string _dataAdi)
        {
            dataAdi = _dataAdi;

            var con_str = new Baglanti(dataAdi).AdmConStr;

            adminData = new AdminData.Yonetim_DbDataContext(con_str);
        }

        public SmsBilgiModel SmsBilgi(int userId)
        {
            var kullanici = adminData.KULLANICI_TANIMLARIs.Where(x => x.ID == userId).SingleOrDefault();

            if (kullanici == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(kullanici.SmsKullaniciAdi) && !string.IsNullOrEmpty(kullanici.SmsSifre))
            {
                var model = new SmsBilgiModel();

                model.SmsFirmaId = kullanici.SmsFirmaId;
                model.SmsKullaniciAdi = kullanici.SmsKullaniciAdi;
                model.SmsSifre = kullanici.SmsSifre;
                model.UserId = userId;
                model.Baslik = kullanici.SmsBaslik;

                return model;
            }

            return null;

        }

        public List<FirmaModel> BeyannaneGonderimIcerikOlustur(List<BeyannameAraModel> clientList, string gonderimTur)
        {

            var liste = new DataIslemler.Sms.BeyannameIslem(dataAdi).GonderimIcerikOlustur(clientList, gonderimTur);

            return liste;
        }

        public List<FirmaModel> BildirgeGonderimIcerikOlustur(List<BildirgeAraModel> clientList, string gonderimTur)
        {

            var liste = new DataIslemler.Sms.BildirgeIslem(dataAdi).GonderimIcerikOlustur(clientList, gonderimTur);

            return liste;
        }
    }
}
