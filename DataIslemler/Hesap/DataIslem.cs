using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.AdminData;
using DataIslemler.Helpers;
using Tacmin.Model;

namespace DataIslemler.Hesap
{
    public class DataIslem
    {
        private Yonetim_DbDataContext data;
        public DataIslem()
        {
            var con_str = new Baglanti("Yonetim_Musavir_Db").AdmConStr;

            data = new Yonetim_DbDataContext(con_str);
        }

        public KayitResModel KullaniciBilgiGuncelle(Tacmin.Model.Hesap.KullaniciModel item)
        {
            var model = data.KULLANICI_TANIMLARIs.Where(x => x.ID == item.Id).SingleOrDefault();

            var res_model = new KayitResModel();

            res_model.IslemDurum = true;

            if (model == null)
            {
                res_model.IslemDurum = false;
                res_model.Mesaj = "Sistemde Beklenmeyen Bir Sorun Oluştu. Tekrar Deneyiniz.";

                return res_model;
            }

            try
            {
                model.ADI = item.Adi;
                model.SOYADI = item.SoyAdi;
                model.GIB_KODU = item.GibKodu;
                model.GIB_SIFRE = item.GibSifre;
                model.GibParola = item.GibParola;
                model.MailKullaniciAdi = item.MailKullaniciAdi;
                model.MailSifre = item.MailSifre;
                model.NTB_KODU = item.NtbKodu;
                model.NTB_SIFRE = item.NtbSifre;
                model.SmsKullaniciAdi = item.SmsKullaniciAdi;
                model.SmsSifre = item.SmsSifre;
                model.SmsFirmaId = item.SmsFirmaId;
                model.SmsBaslik = item.Baslik;
                model.VergiNo = item.VergiNo;
                model.TcNo = item.TcNo;

                data.SubmitChanges();

                res_model.IslemDurum = true;
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message;
            }

            return res_model;
        }
    }
}
