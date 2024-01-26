using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Tebligat
{
    public class TebligatDosyaModel
    {
        public int? Id { get; set; }
        public int? TebligatId { get; set; }
        public string DosyaAdi { get; set; }
        public string Tur { get; set; }
        public string MimeType { get; set; }
        public byte[] Icerik { get; set; }
    }
}
