using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Banka
{
    public class BankaKartListeModel : BankaKartModel
    {
        [DisplayName("Dvz.Cinsi")]
        public string ParaBirimAdi { get; set; }
        [DisplayName("Toplam Yatan")]
        public decimal? Tahsilat { get; set; }
        [DisplayName("Toplam Çekilen")]
        public decimal? Odeme { get; set; }
        [DisplayName("Bakiye")]
        public decimal? Bakiye { get; set; }
        [DisplayName("Hesap Tipi")]
        public string HesapTurAdi { get; set; }

    }
}
