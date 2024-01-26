using System;
using System.Collections.Generic;

namespace DataIslemler.Models
{
    public class TebligatBilgi
    {
        public int Id { get; set; }
        public int FirmaId { get; set; }
        public int BelgeTurId { get; set; }
        public string VdKodu { get; set; }
        public string BirimAdi { get; set; }
        public string BelgeNo { get; set; }
        public string ZarfOid { get; set; }
        public string BelgeOid { get; set; }
        public DateTime? BelgeTarihi { get; set; }
        public DateTime? TebligTarihi { get; set; }

        public List<DosyaBilgi> Dosyalar { get; set; }
    }
}
