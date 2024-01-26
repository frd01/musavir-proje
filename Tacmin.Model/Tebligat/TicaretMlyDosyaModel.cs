using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Tebligat
{
    public class TicaretMlyDosyaModel
    {
        public string oidTeblig { get; set; }
        public string gonderilenDosyaAdi { get; set; }
        public string oid { get; set; }
        public string ekBaseStr { get; set; }
        public byte[] icerik { get; set; }
    }
}
