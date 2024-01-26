using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Kasa
{
    public class KasaEkstraModel
    {
        public int Id { get; set; }
        public string Tarih { get; set; }
        public string IslemTur { get; set; }
        public string Aciklama { get; set; }
        public decimal? GirenTutar { get; set; }
        public decimal? CikanTutar { get; set; }
        public decimal? Bakiye { get; set; }
        public string Kod { get; set; }
    }
}
