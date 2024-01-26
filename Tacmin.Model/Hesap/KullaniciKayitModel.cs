using System.ComponentModel;

namespace Tacmin.Model
{
    public class KullaniciKayitModel
    {
        public int? ID { get; set; }

        [DisplayName("Ad")]
        public string ADI { get; set; }

        [DisplayName("Soyad")]
        public string SOYADI { get; set; }

        [DisplayName("Kodu")]
        public string GIB_KODU { get; set; }

        [DisplayName("Şifre")]
        public string GIB_SIFRE { get; set; }

        [DisplayName("Kodu")]
        public string NTB_KODU { get; set; }

        [DisplayName("Şifre")]
        public string NTB_SIFRE { get; set; }
        [DisplayName("Mail Kullanıcı Adı")]
        public string MailKullaniciAdi { get; set; }
        [DisplayName("Mail Şifre")]
        public string MailSifre { get; set; }
        [DisplayName("Sms Kullanıcı Adı")]
        public string SmsKullaniciAdi { get; set; }
        [DisplayName("Sms Şifre")]
        public string  SmsSifre { get; set; }
    }
}