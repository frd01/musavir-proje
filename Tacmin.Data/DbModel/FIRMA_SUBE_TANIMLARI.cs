using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tacmin.Data.DbModel
{
    public class FIRMA_SUBE_TANIMLARI
    {
        [Key]
        public int ID { get; set; }

        public int? FIRMA_ID { get; set; }

        [Required]
        [ForeignKey("FIRMA_ID")]
        public virtual FIRMA_TANIMLARI FIRMA { get; set; }

        [StringLength(50)]
        public string SUBE_ADI { get; set; }

        [StringLength(30)]
        public string SGK_SUBE_KODU { get; set; }

        [StringLength(11)]
        public string KULLANICI_KODU { get; set; }

        [StringLength(5)]
        public string SUBE_KODU { get; set; }

        [StringLength(15)]
        public string SISTEM_SIFRESI { get; set; }

        [StringLength(15)]
        public string ISYERI_SIFRESI { get; set; }

        public DateTime? ACILIS { get; set; }

        public DateTime? KAPANIS { get; set; }

        public float? BIRIM_MALIYET { get; set; }

        public float? METRE_KARE { get; set; }
    }
}
