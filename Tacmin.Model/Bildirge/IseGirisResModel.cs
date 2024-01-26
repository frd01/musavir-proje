using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Bildirge
{
    public class IseGirisResModel
    {
        public string referans_kodu { get; set; }
        public string tc_no { get; set; }
        public string adi_soyadi { get; set; }
        public DateTime? isten_cikis_tarihi { get; set; }
        public DateTime? islem_tarihi { get; set; }
        public string tur { get; set; }
        public DateTime? giris_tarihi { get; set; }
        public int? firmaId { get; set; }
        public int? subeId { get; set; }
    }
}
