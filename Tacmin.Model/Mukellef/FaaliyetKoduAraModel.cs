using System.ComponentModel;

namespace Tacmin.Model
{
    public class FaaliyetKoduAraModel
    {
        public int ID { get; set; }

        public int? FIRMA_ID { get; set; }

        [DisplayName("Faaliyet Kodu")]
        public string FAALIYET_KODU { get; set; }

        [DisplayName("Açıklama")]
        public string ACIKLAMA { get; set; }
    }
}
