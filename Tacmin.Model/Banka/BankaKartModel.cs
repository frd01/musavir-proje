using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Banka
{
    public class BankaKartModel
    {
        public int Id { get; set; }
        [DisplayName("Banka Kodu")]
        public string BankaKodu { get; set; }
        [DisplayName("Banka Adı")]
        public string BankaAdi { get; set; }
        [DisplayName("Şubesi")]
        public string SubeAdi { get; set; }
        public string SubeKodu { get; set; }
        public int? HesapTurId { get; set; }
        public string HesapAdi { get; set; }
        public string HesapNo { get; set; }
        public string IbanNo { get; set; }
        public int? ParaBirimId { get; set; }
    }
}
