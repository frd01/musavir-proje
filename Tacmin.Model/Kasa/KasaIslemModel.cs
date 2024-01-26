using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Kasa
{
    public class KasaIslemModel
    {
        public int Id { get; set; }
        public int? KasaId { get; set; }
        public int? CariId { get; set; }
        public DateTime? Tarih { get; set; }
        public decimal? Tutar { get; set; }
        public string Aciklama { get; set; }
        public string FisNo { get; set; }
    }
}
