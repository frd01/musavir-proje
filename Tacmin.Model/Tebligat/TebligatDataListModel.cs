using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Tebligat
{
    public class TebligatDataListModel
    {
        public int Id { get; set; }
        [DisplayName("Ünvan")]
        public string Unvan { get; set; }
        public int? FirmaId { get; set; }
        [DisplayName("Belge Türü")]
        public string BelgeTuru { get; set; }
        [DisplayName("Vergi Dairesi")]
        public string VergiDairesi { get; set; }
        [DisplayName("Gönderen Birim")]
        public string BirimAdi { get; set; }
        [DisplayName("Belge No")]
        public string BelgeNo { get; set; }
        [DisplayName("Gönderme Tarihi")]
        public DateTime? GondermeTarihi { get; set; }
        [DisplayName("Tebliğ Tarihi")]
        public DateTime? TebligTarihi { get; set; }
        public List<TebligatDosyaModel> Dosyalar { get; set; }
        public string Durum { get; set; }
        public string durumCss { get; set; }
        public int EkSayisi { get; set; }
        public string cssClass { get; set; }
        public string Whats_App_Css { get; set; }
        public bool? Gonderim_Whats_App { get; set; }
        public string Whats_App_Gonderim_Baslik { get; set; }
    }
}
