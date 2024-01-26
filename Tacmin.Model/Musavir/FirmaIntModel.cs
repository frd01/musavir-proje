using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Musavir
{
    public class FirmaIntModel
    {
        public int id { get; set; }
        public bool is_kullanici { get; set; }
        public string gib_kod { get; set; }
        public string gib_sifre { get; set; }
        public string gib_parola { get; set; }
        public string token { get; set; }
        public string hata_mesaj { get; set; }
        public string vergi_no { get; set; }
        public string tcNo { get; set; }
    }
}
