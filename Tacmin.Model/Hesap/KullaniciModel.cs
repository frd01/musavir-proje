using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Hesap
{
    public class KullaniciModel
    {
        public int? Id { get; set; }
        [DisplayName("Adı")]
        public string Adi { get; set; }
        [DisplayName("Soyadı")]
        public string SoyAdi { get; set; }
        [DisplayName("Gib Kodu")]
        public string GibKodu { get; set; }
        [DisplayName("Gib Şifre")]
        public string GibSifre { get; set; }
        [DisplayName("Gib Parola")]
        public string GibParola { get; set; }
        [DisplayName("Ntb Kodu")]
        public string NtbKodu { get; set; }
        [DisplayName("Ntb Sifre")]
        public string NtbSifre { get; set; }
        [DisplayName("Mail Kullanıcı Adı")]
        public string MailKullaniciAdi { get; set; }
        [DisplayName("Mail Şifre")]
        public string MailSifre { get; set; }
        [DisplayName("Sms Kullanıcı Adı")]
        public string SmsKullaniciAdi { get; set; }
        [DisplayName("Sms Api Secret")]
        public string SmsSifre { get; set; }
        [DisplayName("Sms Sağlayıcı")]
        public int? SmsFirmaId { get; set; }
        [DisplayName("Sms Başlıkları")]
        public string Baslik { get; set; }
        [DisplayName("Vergi No")]
        public string VergiNo { get; set; }
        [DisplayName("Tc No")]
        public string TcNo { get; set; }
    }
}
