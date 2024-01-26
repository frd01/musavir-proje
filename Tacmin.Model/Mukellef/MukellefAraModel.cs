using System.ComponentModel;

namespace Tacmin.Model
{
    public class MukellefAraModel
    {
        public int ID { get; set; }

        [DisplayName("Ünvan")]
        public string UNVAN { get; set; }

        [DisplayName("Vergi Dairesi")]
        public string VERGI_DAIRESI { get; set; }

        [DisplayName("Vergi No")]
        public string VERGI_NO { get; set; }

        [DisplayName("TC No")]
        public string TCKN { get; set; }

        [DisplayName("Grup Kodu")]
        public string GRUP_KODU { get; set; }

        [DisplayName("İl")]
        public string IL { get; set; }

        [DisplayName("İlçe")]
        public string ILCE { get; set; }

        [DisplayName("Aktif")]
        public bool? AKTIF { get; set; }
    }
}
