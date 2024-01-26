using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Maliye
{
    public class AracModel
    {
        public string vdKodu { get; set; }
        public string terkDurum { get; set; }
        [DisplayName("Özel Plaka")]
        public string ozelPlakaNo { get; set; }
        [DisplayName("Cinsi")]
        public string cins { get; set; }
        [DisplayName("Azami Top Ağırlık")]
        public string azamiTopAgirlik { get; set; }
        [DisplayName("Koltuk Sayısı")]
        public string koltukSayisi { get; set; }
        public string tescilDurum { get; set; }
        [DisplayName("Plaka")]
        public string plakaNo { get; set; }
        [DisplayName("Tescil Tarihi")]
        public string tescilTarih { get; set; }
        [DisplayName("Markası")]
        public string marka { get; set; }
        [DisplayName("Satış Tarihi")]
        public string terkTarih { get; set; }
        [DisplayName("Tipi")]
        public string tip { get; set; }
        [DisplayName("Silindir Hacmi")]
        public string silindirHacmi { get; set; }
        [DisplayName("Motor Gücü")]
        public string motorGucu { get; set; }
        [DisplayName("Model")]
        public string model { get; set; }
    }
}
