using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Kasa
{
    public class KasaBakiyeModel
    {
        public int Id { get; set; }
        public string KasaKodu { get; set; }
        public string KasaAdi { get; set; }
        public decimal? Tahsilat { get; set; }
        public decimal? Odeme { get; set; }
        public decimal? Bakiye { get; set; }
    }
}
