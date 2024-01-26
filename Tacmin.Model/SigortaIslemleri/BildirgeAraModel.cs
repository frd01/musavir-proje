using System;
using System.ComponentModel;

namespace Tacmin.Model
{
    public class BildirgeAraModel
    {
        public int ID { get; set; }

        [DisplayName("Vergi No")]
        public string TCKN { get; set; }

        [DisplayName("Ünvan")]
        public string UNVAN { get; set; }

        [DisplayName("Dönem")]
        public DateTime? DONEM { get; set; }

        [DisplayName("Kanun No")]
        public string KANUN_NO { get; set; }

        [DisplayName("Belge Durum")]
        public string BELGE_DURUM { get; set; }

        [DisplayName("Sicil No")]
        public string SICIL_NO { get; set; }

        [DisplayName("Belge Çeşidi")]
        public string BELGE_CESIDI { get; set; }

        [DisplayName("Tutar")]
        public decimal? TUTAR { get; set; }

        [DisplayName("Vade")]
        public DateTime? VADE { get; set; }
        public int? FILE_ID { get; set; }
        public int? TAH_FILE_ID { get; set; }
        public string OID { get; set; }
        public string TAH_OID { get; set; }
        public int? FIRMA_ID { get; set; } 
        [DisplayName("Gönderim")]
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
