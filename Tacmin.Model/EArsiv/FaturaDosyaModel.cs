using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.EArsiv
{
    public class FaturaDosyaModel
    {
        public int Id { get; set; }
        public int? FaturaId { get; set; }
        public byte[] Icerik { get; set; }
        public float? Boyut { get; set; }
        public string DosyaAdi { get; set; }
        public string MimeType { get; set; }
        public string Tur { get; set; }
    }
}
