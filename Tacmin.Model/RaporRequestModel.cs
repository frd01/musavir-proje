using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model
{
    public class RaporRequestModel
    {
        public int? IlkCariId { get; set; }
        public int? SonCariId { get; set; }
        public DateTime? IlkTarih { get; set; }
        public DateTime? SonTarih { get; set; }
        public bool? IsBorcBakiyesi { get; set; }
        public bool? IsAlacakBakiyesi { get; set; }
        public bool? IsBakiyesiz { get; set; }
        public bool? IsYeniSayfa { get; set; }
        public bool? IsNakliYekun { get; set; }
        public bool? IsAktif { get; set; }
    }
}
