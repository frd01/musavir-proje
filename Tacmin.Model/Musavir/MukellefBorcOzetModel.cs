using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Musavir
{
    public class MukellefBorcOzetModel
    {
        public int Id { get; set; }
        [DisplayName("Ünvan")]
        public string Unvan { get; set; }
        [DisplayName("VADESİ GEÇMİŞ BORÇLAR")]
        public decimal? VadesiGecmisBorc { get; set; }
        [DisplayName("VADESİ GELMEMİŞ BORÇLAR")]
        public decimal? VadesiGelmemisBorc { get; set; }
        [DisplayName("TOPLAM BORÇ")]
        public decimal? ToplamBorc { get; set; }
        public List<MukellefBorcDetayModel> VadesiGecmisBorcListesi { get; set; }
        public List<MukellefBorcDetayModel> VadesiGelmemisBorcListesi { get; set; }
    }
}
