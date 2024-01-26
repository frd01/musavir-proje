using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Cari
{
    public class CariListeModel
    {
        public int Id { get; set; }
        [DisplayName("Ünvan")]
        public string Unvan { get; set; }

        public string Telefon { get; set; }
        [DisplayName("Borç Toplam")]
        public decimal? Borc { get; set; }
        [DisplayName("Alacak Toplam")]
        public decimal? Alacak { get; set; }
        [DisplayName("Borç Bakiye")]
        public decimal? BorcBakiye { get; set; }
        [DisplayName("Alacak Bakiye")]
        public decimal? AlacakBakiye { get; set; }
    }
}
