using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Bildirge
{
    public class ViziteListeModel
    {
        public int Id { get; set; }
        [DisplayName("Tc Kimlik No")]
        public string TcNo { get; set; }
        [DisplayName("Ad Soyad")]
        public string AdSoyAd { get; set; }
        [DisplayName("Vaka")]
        public string VakaAdi { get; set; }
        [DisplayName("Rapor Takip No")]
        public string RaporTakipNo { get; set; }
        [DisplayName("Rapor S.No")]
        public int? RaporSiraNo { get; set; }
        [DisplayName("Rapor Başlama Tarihi")]
        public DateTime? RaporBaslamaTarihi { get; set; }
        [DisplayName("İşbaşı/Kontrol Tarihi")]
        public DateTime? IsBasiKontrolTarihi { get; set; }
        [DisplayName("Ceza Durumu")]
        public string CezaDurumu { get; set; }
        public string MedulaId { get; set; }
        public string KullaniciKodu { get; set; }
        public string IsyeriSifresi { get; set; }
        public string IsyeriKodu { get; set; }
        [DisplayName("Ünvan")]
        public string Unvan { get; set; }
        [DisplayName("Şube Adı")]
        public string SubeAdi { get; set; }
        public int? FirmaId { get; set; }
        public int? SubeId { get; set; }
        public string  Vaka { get; set; }
    }
}
