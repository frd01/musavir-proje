using System;
using System.ComponentModel;

namespace Tacmin.Data.DbModel
{
    public class Giden_Faturalar
    {
        public int Id { get; set; }
        public int Firma_Id { get; set; }

        public DateTime? Tarih { get; set; }
        [DisplayName("Fatura No")]
        public string Fatura_No { get; set; }
        [DisplayName("Ünvan")]
        public string Unvan { get; set; }
        [DisplayName("Vergi Dairesi")]
        public string Vergi_Dairesi { get; set; }
        [DisplayName("Vergi No")]
        public string Vergi_No { get; set; }
        [DisplayName("Belge Turu")]
        public string Belge_Turu { get; set; }
        [DisplayName("Onay Durumu")]
        public string Onay_Durumu { get; set; }
        [DisplayName("Ettn No")]
        public string Ettn_No { get; set; }
        [DisplayName("Mal Hizmet Toplamı")]
        public double? Mal_Hizmet_Toplami { get; set; }
        [DisplayName("Toplam İskonta")]
        public double? Iskonta_Toplam { get; set; }
        public double? Kdv { get; set; }
        [DisplayName("Vergiler Dahil Toplam")]
        public double? Vergiler_Dahil_Toplam { get; set; }
        [DisplayName("Ödenecek Tutar")]
        public double? Odenecek_Toplam { get; set; }
        public string Senorya { get; set; }
        [DisplayName("Fatura Tipi")]
        public string Fatura_Tipi { get; set; }
        [DisplayName("Tevfikata Tabi İşlem Tutarı")]
        public double? Tevfikat_Tabi_Islem_Tutar { get; set; }
        [DisplayName("Hesaplanan Tevfikat Kdv")]
        public double? Tevfikat_Hesaplan_Kdv { get; set; }
        [DisplayName("Tevfikata Tabi İşlem Üze.Hes.Kdv")]
        public double? Tevfikat_Tabi_Hesaplanan_Kdv { get; set; }
        public double? Kdv_Oran { get; set; }
        public double? Tevfikat_Oran { get; set; }
        public double? KDV_0 { get; set; }
        public double? Kdv_1 { get; set; }
        public double? Kdv_8 { get; set; }
        public double? Kdv_18 { get; set; }
        public double? Kdv_10 { get; set; }
        public double? Kdv_20 { get; set; }
    }
}
