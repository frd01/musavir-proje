using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.LocalData
{
    public class FirmaModel
    {
        // İndirilenler
        public int? Id { get; set; }
        public string Unvan { get; set; }
        public string GibKodu { get; set; }
        public string GibParola { get; set; }
        public string GibSifre { get; set; }
    }
}
