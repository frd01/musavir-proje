using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Banka
{
    public class BankaEsktraModel
    {
        public int Id { get; set; }
        public string Tarih { get; set; }
        public string IslemTur { get; set; }
        public string Kodu { get; set; }
        public string Aciklama { get; set; }
        public decimal? Borc { get; set; }
        public decimal? Alacak { get; set; }
        public decimal? Bakiye { get; set; }
        public int? CariId { get; set; }
        public int? KasaId { get; set; }
    }
}
