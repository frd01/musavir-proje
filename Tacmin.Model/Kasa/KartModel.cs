using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Kasa
{
    public class KartModel
    {
        public int Id { get; set; }
        [DisplayName("Kasa Kodu")]
        public string KasaKodu { get; set; }
        [DisplayName("Kasa Adı")]
        public string KasaAdi { get; set; }
        public int? ParaBirimId { get; set; }
        public string OzelKod { get; set; }
        [DisplayName("Kasa Açıklaması")]
        public string Aciklama { get; set; }
        public string MuhasebeKodu { get; set; }
    }
}
