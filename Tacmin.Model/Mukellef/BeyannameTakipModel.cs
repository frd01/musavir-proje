using System.ComponentModel;

namespace Tacmin.Model.Mukellef
{
    public class BeyannameTakipModel
    {
        public int Id { get; set; }
        [DisplayName("Ünvan")]
        public string Unvan { get; set; }
        [DisplayName("KDV1")]
        public BeyannameDonemModel Kdv1 { get; set; }
        [DisplayName("KDV2")]
        public BeyannameDonemModel Kdv2 { get; set; }
        [DisplayName("KDV4")]
        public BeyannameDonemModel Kdv4 { get; set; }
        [DisplayName("MUHSGK")]
        public BeyannameDonemModel Muh_Sgk { get; set; }
        [DisplayName("MUHSGK2")]
        public BeyannameDonemModel Muh_Sgk2 { get; set; }
        [DisplayName("GGECICI")]
        public BeyannameDonemModel G_Gecici { get; set; }
        [DisplayName("KGECICI")]
        public BeyannameDonemModel K_Gecici { get; set; }
        [DisplayName("Ba")]
        public BeyannameDonemModel Ba { get; set; }
        [DisplayName("Bs")]
        public BeyannameDonemModel Bs { get; set; }
        [DisplayName("GELIR")]
        public BeyannameDonemModel Gelir { get; set; }
        [DisplayName("GELIR1001E")]
        public BeyannameDonemModel Gelir_1001E { get; set; }
        [DisplayName("KURUMLAR")]
        public BeyannameDonemModel Kurumlar { get; set; }
        [DisplayName("ÖTV1")]
        public BeyannameDonemModel Otv1 { get; set; }
        [DisplayName("ÖTV3A")]
        public BeyannameDonemModel Otv3A { get; set; }
        [DisplayName("ÖTV3B")]
        public BeyannameDonemModel Otv3B { get; set; }
        [DisplayName("OTV4")]
        public BeyannameDonemModel Otv4 { get; set; }
        [DisplayName("ÖIV")]
        public BeyannameDonemModel Oiv { get; set; }
        [DisplayName("TURIZM")]
        public BeyannameDonemModel Turizm { get; set; }
        [DisplayName("POSET")]
        public BeyannameDonemModel Poset { get; set; }
        [DisplayName("DAMGA")]
        public BeyannameDonemModel Damga { get; set; }

    }
}
