using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.EArsiv
{
    public class ArsivFaturaModel
    {
        public int? Id { get; set; }
        public int? firmaId { get; set; }
        [DisplayName("Ünvan")]
        public string Unvan { get; set; }
        [DisplayName("Alıcı Unvan")]
        public string aliciUnvan { get; set; }
        [DisplayName("Vergi No")]
        public string aliciVergiNo { get; set; }
        [DisplayName("Fatura No")]
        public string belgeNo { get; set; }
        [DisplayName("Belge Türü")]
        public string belgeTuru { get; set; }
        [DisplayName("Ettn No")]
        public string ettn { get; set; }
        public string htmlText { get; set; }
        [DisplayName("Mal Hizmet Toplam")]
        public decimal? malHizmetToplam { get; set; }
        [DisplayName("Ödenecek")]
        public decimal? odenecek { get; set; }
        [DisplayName("Onay Durumu")]
        public string onayDurumu { get; set; }
        [DisplayName("Para Birimi")]
        public string paraBirimi { get; set; }
        [DisplayName("Senaryo")]
        public string senaryo { get; set; }
        [DisplayName("Tarih")]
        public DateTime? tarih { get; set; }
        [DisplayName("Tarih")]
        public string tip { get; set; }
        [DisplayName("Vergiler Dahil Toplam")]
        public decimal? vergidahilToplam { get; set; }
        [DisplayName("Matrah(0)")]
        public decimal? hesaplananKdv_0 { get; set; }
        [DisplayName("Matrah(1)")]
        public decimal? hesaplananKdv_1 { get; set; }
        [DisplayName("Matrah(8)")]
        public decimal? hesaplananKdv_8 { get; set; }
        [DisplayName("Matrah(10)")]
        public decimal? hesaplananKdv_10 { get; set; }
        [DisplayName("Matrah(18)")]
        public decimal? hesaplananKdv_18 { get; set; }
        [DisplayName("Matrah(20)")]
        public decimal? hesaplananKdv_20 { get; set; }
        [DisplayName("Kdv(0)")]
        public decimal? kdvMatrah_0 { get; set; }
        [DisplayName("Kdv(1)")]
        public decimal? kdvMatrah_1 { get; set; }
        [DisplayName("Kdv(8)")]
        public decimal? kdvMatrah_8 { get; set; }
        [DisplayName("Kdv(10)")]
        public decimal? kdvMatrah_10 { get; set; }
        [DisplayName("Kdv(18)")]
        public decimal? kdvMatrah_18 { get; set; }
        [DisplayName("Kdv(20)")]
        public decimal? kdvMatrah_20 { get; set; }
        [DisplayName("İskonto Toplam")]
        public decimal? iskontoToplam { get; set; }
        [DisplayName("Tevfikat Tutarı")]
        public decimal? tevfikatKdvTutar { get; set; }
        [DisplayName("Kdv")]
        public decimal? KdvToplam { get; set; }

    }

    
}
