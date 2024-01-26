using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model
{
    public class DosyaModel
    {
        public string DosyaAdi { get; set; }
        public string MimeType { get; set; }
        public byte[] Icerik { get; set; }
    }
}
