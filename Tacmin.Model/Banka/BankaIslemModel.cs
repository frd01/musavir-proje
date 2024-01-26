using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Banka
{
    public class BankaIslemModel
    {
        public int? Id { get; set; }
        public DateTime? Tarih { get; set; }
        public string FisNo { get; set; }
        public int? KasaId { get; set; }
        public int? BankaId { get; set; }
        public int? CariId { get; set; }
        public string Aciklama { get; set; }
        public string Islem { get; set; }
        public decimal? Tutar { get; set; }
        public string CariKodu { get; set; }
        public string BankaKodu { get; set; }
        public string KasaKodu { get; set; }
    }
}
