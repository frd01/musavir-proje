using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Banka
{
    public class BankaBakiyeRaporModel
    {
        public int Id { get; set; }
        public string BankaKodu { get; set; }
        public string BankaAdi { get; set; }
        public string SubeAdi { get; set; }
        public string IbanNo { get; set; }
        public decimal? Borc { get; set; }
        public decimal? Alacak { get; set; }
        public decimal? Bakiye { get; set; }
    }
}
