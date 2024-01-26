using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tacmin.Model.Tebligat
{
    public class GelirTebligatListe
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Unvan")]
        public string UNVAN { get; set; }
        [DisplayName("Gönderen Birim")]
        public string BirimAdi { get; set; }
        [DisplayName("Belge Türü")]
        public string Belge_Turu { get; set; }
        [DisplayName("Gönderme Tarihi")]
        public DateTime? GondermeTarihi { get; set; }
        [DisplayName("Tebliğ Tarihi")]
        public DateTime? TebligTarihi { get; set; }
        [DisplayName("Belge No")]
        public string BelgeNo { get; set; }
    }
}
