using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tacmin.Data.DbModel;

namespace Tacmin.Model
{
    public class MukellefKayitModel
    {
        public int? ID { get; set; }

        [DisplayName("Ünvan")]
        [Required]
        public string UNVAN { get; set; }

        [DisplayName("Vergi Dairesi")]
        public string VERGI_DAIRESI { get; set; }

        [DisplayName("Vergi No")]
        public string VERGI_NO { get; set; }

        [DisplayName("TC No")]
        public string TCKN { get; set; }

        [DisplayName("Kodu")]
        public string GIB_KODU { get; set; }

        [DisplayName("Parola")]
        public string GIB_PAROLA { get; set; }

        [DisplayName("Şifre")]
        public string GIB_SIFRE { get; set; }

        [DisplayName("Aktif")]
        public bool? AKTIF { get; set; }

        [DisplayName("Grup Kodu")]
        public string GRUP_KODU { get; set; }

        [DisplayName("Kodu")]
        public string EDEVLET_KODU { get; set; }

        [DisplayName("Şifre")]
        public string EDEVLET_SIFRE { get; set; }

        [DisplayName("Adres")]
        public string ADRES { get; set; }

        [DisplayName("İl")]
        public string IL { get; set; }

        [DisplayName("İlçe")]
        public string ILCE { get; set; }

        [DisplayName("Firma Açılış")]
        public DateTime? ACILIS { get; set; }

        [DisplayName("Firma Kapanış")]
        public DateTime? KAPANIS { get; set; }

        [DisplayName("Muh. Başlangıç")]
        public DateTime? MUH_BASLANGIC { get; set; }

        [DisplayName("Muh. Bitiş")]
        public DateTime? MUH_BITIS { get; set; }
        public int? Sehir_Id { get; set; }
        public int? Ilce_Id { get; set; }
        public int? Vergi_Daire_Id { get; set; }

        public virtual ICollection<Firma_Iletisim> Firma_Iletisim { get; set; }

    }
}
