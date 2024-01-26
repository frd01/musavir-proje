using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Kasa
{
    public class KasaIslemListeModel
    {
        public int Id { get; set; }
        public int? KasaId { get; set; }
        public string KasaKodu { get; set; }
        public string KasaAdi { get; set; }
        public int? CariId { get; set; }
        public string CariAdi { get; set; }
        public DateTime? Tarih { get; set; }
        public decimal? GirenTutar { get; set; }
        public decimal? CikanTutar { get; set; }
        public decimal? Bakiye { get; set; }
        public string Aciklama { get; set; }
        public string IslemTur { get; set; }
        public string Modul { get; set; }
    }
}
