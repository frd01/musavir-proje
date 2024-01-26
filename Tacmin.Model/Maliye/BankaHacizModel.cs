using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Maliye
{
    public class BankaHacizModel
    {
        [DisplayName("Durum")]
        public string durum { get; set; }
        [DisplayName("Haciz Bildirisi No")]
        public string hbno { get; set; }
        public string borcDurum { get; set; }
        [DisplayName("Haciz Bildirisi Tutarı (TL)")]
        public decimal? htutar { get; set; }
        public string vdkod { get; set; }
        [DisplayName("Vergi Dairesi Adı")]
        public string vergiDairesi { get; set; }
    }
}
