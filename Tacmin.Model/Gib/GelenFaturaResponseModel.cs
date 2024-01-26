using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Gib
{
    public class GelenFaturaResponseModel
    {
        public GelenFaturaResponseModelData data { get; set; }
    }

    public class GelenFaturaResponseModelData
    {

        public List<GelenFaturaFaturaSonuc> FATURASONUC { get; set; }
        public bool GELEN { get; set; }
    }

    public class GelenFaturaFaturaSonuc 
    {
        public string faturaNo { get; set; }
        public string toplam { get; set; }
        public string mukVkn { get; set; }
        public string tarih { get; set; }
        public string paraBirimi { get; set; }
        public string tesisatNo { get; set; }
        public string odenecek { get; set; }
        public string vergi { get; set; }
        public string unvan { get; set; }
        public string gonderimSekli { get; set; }
    }
}
