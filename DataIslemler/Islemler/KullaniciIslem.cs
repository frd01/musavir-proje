using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.AdminData;
using DataIslemler.Helpers;
using DataIslemler.Models;
using Tacmin.Model;

namespace DataIslemler.Islemler
{
    public class KullaniciIslem
    {
        Yonetim_DbDataContext data; 


        public KullaniciIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).AdmConStr;

            data = new Yonetim_DbDataContext(con_str);
        }

        public Tacmin.Data.DbModel.KULLANICI_TANIMLARI GetDataKullaniciModel(string kullaniciKodu, string sifreMd5)
        {
            var item = data.KULLANICI_TANIMLARIs.Where(x => x.KULLANICI_KODU == kullaniciKodu && x.SIFRE == sifreMd5).SingleOrDefault();

            if (item != null)
            {
                var model = new Tacmin.Data.DbModel.KULLANICI_TANIMLARI();
                model.ADI = item.ADI;
                model.AKTIF = item.AKTIF;
                model.DATA_ADI = item.DATA_ADI;
                model.GIB_KODU = item.GIB_KODU;
                model.GIB_SIFRE = item.GIB_SIFRE;
                model.ID = item.ID;
                model.KULLANICI_KODU = item.KULLANICI_KODU;
                model.MailKullaniciAdi = item.MailKullaniciAdi;
                model.MailSifre = item.MailSifre;
                model.NTB_KODU = item.NTB_KODU;
                model.NTB_SIFRE = item.NTB_SIFRE;
                model.SIFRE = item.SIFRE;
                model.SmsKullaniciAdi = item.SmsKullaniciAdi;
                model.SmsSifre = item.SmsSifre;
                model.SOYADI = item.SOYADI;
                model.YETKILI = item.YETKILI;

                return model;

            }

            return null;

        }

        public UserModel GetKullaniciModel(string kullaniciKodu)
        {

            var kontrol = data.KULLANICI_TANIMLARIs.Where(x =>  x.KULLANICI_KODU == kullaniciKodu).Count();

            if (kontrol <= 0)
                return null;

            var data_model = data.GetKullaniciListesi().Where(x =>  x.KULLANICI_KODU == kullaniciKodu).SingleOrDefault();

            var model = new UserModel();
            model.Adi = data_model.ADI;
            model.SoyAdi = data_model.SOYADI;
            model.DataAdi = data_model.DATA_ADI;
            model.GibKodu = data_model.GIB_KODU;
            model.GibSifre = data_model.GIB_SIFRE;
            model.Id = data_model.ID;
            model.KullaniciKodu = data_model.KULLANICI_KODU;
            model.NtbKodu = data_model.NTB_KODU;
            model.NtbSifre = data_model.NTB_SIFRE;
            model.Yetkiler = data_model.YETKILER;
            model.Yetkili = data_model.YETKILI;
            model.Sifre = data_model.SIFRE;

            return model;
        }

        public void KullaniciBilgiGuncelle(KullaniciKayitModel item)
        {
            var model = data.KULLANICI_TANIMLARIs.Where(x => x.ID == item.ID).SingleOrDefault();
            model.AD = item.ADI;
            model.ADI = item.ADI;
            model.SOYAD = item.SOYADI;
            model.SOYADI = item.SOYADI;
            model.GIB_SIFRE = item.GIB_SIFRE;
            model.GIB_KODU = item.GIB_KODU;
            model.NTB_KODU = item.NTB_KODU;
            model.NTB_SIFRE = item.NTB_SIFRE;

            data.SubmitChanges();
        }
        
    }
}
