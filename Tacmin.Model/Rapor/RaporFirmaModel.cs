using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Rapor
{
    public class RaporFirmaModel
    {
        public int Id { get; set; }
        [DisplayName("Ünvan")]
        public string Unvan { get; set; }
        [DisplayName("Açıklama")]
        public string Mesaj { get; set; }
        public int? FirmaId { get; set; }

    }
}
