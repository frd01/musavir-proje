using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Musavir
{
    public class MukellefBorcDetayModel
    {
        public int Id { get; set; }
        [DisplayName("Ünvan")]
        public string Unvan { get; set; }
        [DisplayName("Vergi Kodu")]
        public string VergiKodu { get; set; }
        [DisplayName("Vergi Adı")]
        public string VergiAdi { get; set; }
        [DisplayName("Asıl Borç")]
        public decimal? AnaBorc { get; set; }
        [DisplayName("Gecikme Zammı")]
        public decimal?  GecikmeZammi { get; set; }
        [DisplayName("Toplam")]
        public decimal? Toplam { get; set; }
    }
}
