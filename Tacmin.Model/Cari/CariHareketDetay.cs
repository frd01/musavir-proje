using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Cari
{
    public class CariHareketDetay
    {
        public int Id { get; set; }
        public int ModulId { get; set; }
        [DisplayName("Modül")]
        public string ModulAdi { get; set; }
        public DateTime? Tarih { get; set; }
        [DisplayName("İşlem Açıklama")]
        public string Aciklama { get; set; }
        [DisplayName("Borç")]
        public decimal? Borc { get; set; }
        public decimal? Alacak { get; set; }
        public decimal? Bakiye { get; set; }
    }
}
