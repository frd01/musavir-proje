using System;
using System.ComponentModel.DataAnnotations;

namespace Tacmin.Data.DbModel
{
    public class GELEN_FATURALAR
    {
        [Key]
        public int ID { get; set; }
        public string FATURA_NO { get; set; }
        public string VERGI_NO { get; set; }
        public DateTime? TARIH { get; set; }
        public string TESISAT_NO { get; set; }
        public string UNVAN { get; set; }
        public string GONDERIM_SEKLI { get; set; }
        public decimal? TOPLAM { get; set; }
        public decimal? VERGI { get; set; }
        public decimal? ODENECEK { get; set; }
        public decimal? KDV_0 { get; set; }
        public decimal? TOPLAM_0 { get; set; }
        public decimal? KDV_1 { get; set; }
        public decimal? TOPLAM_1 { get; set; }
        public decimal? KDV_10 { get; set; }
        public decimal? TOPLAM_10 { get; set; }
        public decimal? KDV_20 { get; set; }
        public decimal? TOPLAM_20 { get; set; }
        [Required]
        public int FIRMA_ID { get; set; }
    }
}
