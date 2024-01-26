using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tacmin.Data.DbModel
{
    public class KULLANICI_TANIMLARI
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string KULLANICI_KODU { get; set; }

        [StringLength(32)]
        public string SIFRE { get; set; }

        public bool? AKTIF { get; set; }

        public string YETKILER { get; set; }

        public bool? YETKILI { get; set; }

        [StringLength(70)]
        public string ADI { get; set; }

        [StringLength(70)]
        public string SOYADI { get; set; }

        [StringLength(10)]
        public string GIB_KODU { get; set; }

        [StringLength(10)]
        public string GIB_SIFRE { get; set; }

        [StringLength(11)]
        public string NTB_KODU { get; set; }

        [StringLength(10)]
        public string NTB_SIFRE { get; set; }
        public string DATA_ADI { get; set; }
        public string MailKullaniciAdi { get; set; }
        public string MailSifre { get; set; }
        public string SmsKullaniciAdi { get; set; }
        public string SmsSifre { get; set; }

        public virtual IEnumerable<FIRMA_BAGLANTILARI> FIRMA_BAGLANTILARI { get; set; }
    }
}