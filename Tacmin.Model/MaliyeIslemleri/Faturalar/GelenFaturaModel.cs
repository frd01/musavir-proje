using System;
using System.ComponentModel;

namespace Tacmin.Model.MaliyeIslemleri.Faturalar
{
    public class GelenFaturaModel
    {

        public int ID { get; set; }
        [DisplayName("Fatura No")]
        public string FATURA_NO { get; set; }
        [DisplayName("Vergi No")]
        public string VERGI_NO { get; set; }
        [DisplayName("Tarih")]
        public DateTime? TARIH { get; set; }
        [DisplayName("Tesisat No")]
        public string TESISAT_NO { get; set; }
        [DisplayName("Ünvan")]
        public string UNVAN { get; set; }
        [DisplayName("Gönderim Şekli")]
        public string GONDERIM_SEKLI { get; set; }
        [DisplayName("Toplam")]
        public decimal? TOPLAM { get; set; }
        [DisplayName("Vergi")]
        public decimal? VERGI { get; set; }
        [DisplayName("Ödenecek Tutar")]
        public decimal? ODENECEK { get; set; }
        [DisplayName("Kdv (0)")]
        public decimal? KDV_0 { get; set; }
        [DisplayName("Matrah (0)")]
        public decimal? TOPLAM_0 { get; set; }
        [DisplayName("Kdv (1)")]
        public decimal? KDV_1 { get; set; }
        [DisplayName("Matrah (1)")]
        public decimal? TOPLAM_1 { get; set; }
        [DisplayName("Kdv (10)")]
        public decimal? KDV_10 { get; set; }
        [DisplayName("Matrah (10)")]
        public decimal? TOPLAM_10 { get; set; }
        [DisplayName("Kdv (20)")]
        public decimal? KDV_20 { get; set; }
        [DisplayName("Matrah (20)")]
        public decimal? TOPLAM_20 { get; set; }
    }
}
