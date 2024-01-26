using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tacmin.Data.DbModel;

namespace Tacmin.Data
{
    public class BEYANNAME_ICERIKLERI
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int? FIRMA_ID { get; set; }

        [ForeignKey("FIRMA_ID")]
        public virtual FIRMA_TANIMLARI FIRMA { get; set; }

        [StringLength(50)]
        public string OID { get; set; }

        public int? SUBE_NO { get; set; }

        [StringLength(50)]
        public string TAH_OID { get; set; }

        [StringLength(50)]
        public string KODU { get; set; }

        public DateTime? GONDERIM_TARIHI { get; set; }

        public DateTime? DONEM_BAS { get; set; }

        public DateTime? DONEM_SON { get; set; }

        [StringLength(50)]
        public string VERGI_DAIRESI { get; set; }

        [StringLength(150)]
        public string UNVAN { get; set; }

        public string TCKN { get; set; }

        [StringLength(50)]
        public string DURUM { get; set; }

        [StringLength(50)]
        public string TUR { get; set; }

        [StringLength(100)]
        public string TAH_FIS_NO { get; set; }

        public DateTime? TAH_VADE { get; set; }

        public decimal? TAH_TUTAR { get; set; }

        public short? IHBARNAME_KESILDI { get; set; }

        public short? MESAJ_VAR { get; set; }

        public int? FILE_ID { get; set; }

        [ForeignKey("FILE_ID")]
        public virtual DOSYA DOSYA { get; set; }

        public int? TAH_FILE_ID { get; set; }

        [ForeignKey("TAH_FILE_ID")]
        public virtual DOSYA TAH_DOSYA { get; set; }
        public bool? Gonderim_Whats_App { get; set; }
        public bool? Gonderim_Mail { get; set; }
        public bool? Gonderim_Sms { get; set; }
        public string Whats_App_Css { get; set; }
        public string Whats_App_Gonderim_Baslik { get; set; }        
        public string Mail_Css { get; set; }
        public string Mail_Gonderim_Baslik { get; set; }
        public string PanelGonderim { get; set; }
        public string Sms_Baslik { get; set; }
        public string Sms_Css { get; set; }
    }
}
