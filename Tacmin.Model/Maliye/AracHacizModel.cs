using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Maliye
{
    public class AracHacizModel
    {
        [DisplayName("Haciz Bildirisi Tutarı (TL)")]
        public decimal? hacizBildTutar { get; set; }
        public string durum { get; set; }
        [DisplayName("Haciz Bildirisi No")]
        public string hacizBildiriNo { get; set; }
        public string orgOid { get; set; }
        [DisplayName("Durum")]
        public string borcDurum { get; set; }
        public string vdkod { get; set; }
        [DisplayName("Vergi Dairesi Adı")]
        public string vergiDairesi { get; set; }
    }
}
