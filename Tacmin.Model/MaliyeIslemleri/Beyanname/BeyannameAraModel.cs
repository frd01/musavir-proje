using System;
using System.ComponentModel;

namespace Tacmin.Model
{
    public class BeyannameAraModel
    {
        public int ID { get; set; }

        [DisplayName("Ünvan")]
        public string UNVAN { get; set; }

        [DisplayName("Tür")]
        public string TUR { get; set; }

        [DisplayName("Vergi Dairesi")]
        public string VERGI_DAIRESI { get; set; }

        [DisplayName("Vergi No")]
        public string TCKN { get; set; }

        [DisplayName("İlk Dönem")]
        public DateTime? DONEM_BAS { get; set; }

        [DisplayName("Son Dönem")]
        public DateTime? DONEM_SON { get; set; }

        [DisplayName("Dönem")]
        public string DONEM => DONEM_BAS?.ToString("MM-yyyy") + "-" + DONEM_SON?.ToString("MM-yyyy");

        [DisplayName("Fiş No")]
        public string TAH_FIS_NO { get; set; }

        [DisplayName("Vade")]
        public DateTime? TAH_VADE { get; set; }

        [DisplayName("Tutar")]
        public decimal? TAH_TUTAR { get; set; }

        [DisplayName("Gönderim Tarihi")]
        public DateTime? GONDERIM_TARIHI { get; set; }

        public int? FILE_ID { get; set; }

        public int? TAH_FILE_ID { get; set; }

        [DisplayName("Kodu")]
        public string KODU { get; set; }

        public int? FIRMA_ID { get; set; }

        public string OID { get; set; }
        public string TAH_OID { get; set; }
       
        public bool? Gonderim_Whats_App { get; set; }
        public string Whats_App_Css { get; set; }
        
        public string Whats_App_Gonderim_Baslik { get; set; }
        public bool? Gonderim_Mail { get; set; }
        public string Mail_Css { get; set; }
        public string Mail_Gonderim_Baslik { get; set; }
        [DisplayName("Gönderim")]
        public string PanelGonderim { get; set; }
        public string Sms_Css { get; set; }
        public string Sms_Baslik { get; set; }
        public bool? Gonderim_Sms { get; set; }
    }
}
