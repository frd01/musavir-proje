using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.AdminData;
using DataIslemler.Helpers;
using Tacmin.Model;
using Tacmin.Model.Sms;

namespace DataIslemler.Hesap
{
    public class ListeIslem
    {
        private Yonetim_DbDataContext data;

        public ListeIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).AdmConStr;

            data = new Yonetim_DbDataContext(con_str);
        }

        public Tacmin.Model.Hesap.KullaniciModel GetKullanici(int? kullaniciId)
        {
            var data_model = data.KULLANICI_TANIMLARIs.Where(x => x.ID == kullaniciId).SingleOrDefault();

            var model = new Tacmin.Model.Hesap.KullaniciModel();

            if (data_model != null)
            {
                model.Adi = data_model.ADI;
                model.SoyAdi = data_model.SOYADI;
                model.GibKodu = data_model.GIB_KODU;
                model.GibSifre = data_model.GIB_SIFRE;
                model.GibParola = data_model.GibParola;
                model.Id = data_model.ID;
                model.MailKullaniciAdi = data_model.MailKullaniciAdi;
                model.MailSifre = data_model.MailSifre;
                model.NtbKodu = data_model.NTB_KODU;
                model.NtbSifre = data_model.NTB_SIFRE;
                model.SmsSifre = data_model.SmsSifre;
                model.SmsKullaniciAdi = data_model.SmsKullaniciAdi;
                model.SmsFirmaId = data_model.SmsFirmaId;
                model.Baslik = data_model.SmsBaslik;
                model.VergiNo = data_model.VergiNo;
                model.TcNo = data_model.TcNo;
            }

            return model;
        }

        public List<SmsFirmaModel> SmsFirmaListesi()
        {
            var liste = new List<SmsFirmaModel>();

            foreach (var item in data.SmsFirmalars)
            {
                var model = new SmsFirmaModel();

                model.Id = item.Id;
                model.FirmaAdi = item.FirmaAdi;

                liste.Add(model);
            }

            return liste;
        }
    }
}
