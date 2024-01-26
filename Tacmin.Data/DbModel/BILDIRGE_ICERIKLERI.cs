using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tacmin.Data.DbModel
{
    public class BILDIRGE_ICERIKLERI
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int? FIRMA_ID { get; set; }

        [ForeignKey("FIRMA_ID")]
        public virtual FIRMA_TANIMLARI FIRMA { get; set; }

        [StringLength(50)]
        public string OID { get; set; }

        [StringLength(50)]
        public string TAH_OID { get; set; }

        public string TCKN { get; set; }

        [StringLength(150)]
        public string UNVAN { get; set; }

        public DateTime? DONEM { get; set; }

        [StringLength(50)]
        public string KANUN_NO { get; set; }

        [StringLength(50)]
        public string BELGE_DURUM { get; set; }

        [StringLength(50)]
        public string SICIL_NO { get; set; }

        [StringLength(50)]
        public string BELGE_CESIDI { get; set; }

        public decimal? TUTAR { get; set; }

        public DateTime? VADE { get; set; }

        public int? FILE_ID { get; set; }

        [ForeignKey("FILE_ID")]
        public virtual DOSYA DOSYA { get; set; }

        public int? TAH_FILE_ID { get; set; }

        [ForeignKey("TAH_FILE_ID")]
        public virtual DOSYA TAH_DOSYA { get; set; }
        //public string DonemStr { get; set; }
        public bool? Gonderim_Whats_App { get; set; }
        public bool? Gonderim_Mail { get; set; }
        public bool? Gonderim_Sms { get; set; }
        public string Whats_App_Css { get; set; }
        public string Whats_App_Gonderim_Baslik { get; set; }

        public string Mail_Css { get; set; }
        public string Mail_Gonderim_Baslik { get; set; }
       
        public string PanelGonderim { get; set; }
        public string Sms_Css { get; set; }
        public string Sms_Baslik { get; set; }
        

    }
}
