using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Kasa
{
    public class KartListeModel : KartModel
    {
        [DisplayName("Dvz.Cinsi")]
        public string ParaBirimi { get; set; }
        [DisplayName("Toplam Tahsilat")]
        public decimal? Tahsilat { get; set; }
        [DisplayName("Toplam Ödeme")]
        public decimal? Odeme { get; set; }
        [DisplayName("Bakiye")]
        public decimal? Bakiye { get; set; }
    }
}
