using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tacmin.Data.DbModel
{
    public class FIRMA_TANIMLARI
    {
        [Key]
        public int ID { get; set; }

        [StringLength(200)]
        public string UNVAN { get; set; }

        [StringLength(50)]
        public string VERGI_DAIRESI { get; set; }

        public string VERGI_NO { get; set; }

        public string TCKN { get; set; }

        [StringLength(15)]
        public string GIB_KODU { get; set; }

        [StringLength(15)]
        public string GIB_PAROLA { get; set; }

        [StringLength(15)]
        public string GIB_SIFRE { get; set; }

        public bool? AKTIF { get; set; }

        [StringLength(20)]
        public string GRUP_KODU { get; set; }

        [StringLength(11)]
        public string EDEVLET_KODU { get; set; }

        [StringLength(25)]
        public string EDEVLET_SIFRE { get; set; }

        [StringLength(250)]
        public string ADRES { get; set; }

        [StringLength(20)]
        public string IL { get; set; }

        [StringLength(30)]
        public string ILCE { get; set; }

        public DateTime? ACILIS { get; set; }

        public DateTime? KAPANIS { get; set; }

        public DateTime? MUH_BASLANGIC { get; set; }

        public DateTime? MUH_BITIS { get; set; }
        public int? Sehir_Id { get; set; }
        public int? Ilce_Id { get; set; }
        public int? Vergi_Daire_Id { get; set; }

        public virtual ICollection<FIRMA_SUBE_TANIMLARI> SUBE_TANIMLARI { get; set; }

        public virtual ICollection<BEYANNAME_ICERIKLERI> BEYANNAMELER { get; set; }

        public virtual ICollection<BILDIRGE_ICERIKLERI> BILDIRGELER { get; set; }

        public virtual ICollection<Firma_Iletisim> Firma_Iletisim { get; set; }

    }
}
