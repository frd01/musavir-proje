using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tacmin.Model
{
    public class FirmaSubeKayitModel
    {
        public int? ID { get; set; }

        public int? FIRMA_ID { get; set; }

        [DisplayName("Şube Adı")]
        [Required]
        public string SUBE_ADI { get; set; }

        [DisplayName("SGK Kodu")]
        public string SGK_SUBE_KODU { get; set; }

        [DisplayName("Kullanıcı Kodu")]
        public string KULLANICI_KODU { get; set; }

        [DisplayName("Şube Kodu")]
        public string SUBE_KODU { get; set; }

        [DisplayName("Sistem Şifresi")]
        public string SISTEM_SIFRESI { get; set; }

        [DisplayName("İşyeri Şifresi")]
        public string ISYERI_SIFRESI { get; set; }

        [DisplayName("Açılış")]
        public DateTime? ACILIS { get; set; }

        [DisplayName("Kapanış")]
        public DateTime? KAPANIS { get; set; }

        [DisplayName("Birim Maliyet")]
        public float? BIRIM_MALIYET { get; set; }

        [DisplayName("Metre Kare")]
        public float? METRE_KARE { get; set; }
    }
}
