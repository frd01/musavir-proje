using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Maliye
{
    public class GelenFaturaModel_Data_1
    {
        public GelenFaturaModel_Data_2 data { get; set; }
    }

    public class GelenFaturaModel_Data_2
    {
        public List<GelenFaturaModel> FATURASONUC { get; set; }
    }
    public class GelenFaturaModel
    {
        public string faturaNo { get; set; }
        public decimal? toplam { get; set; }
        public string mukVkn { get; set; }
        public string tarih { get; set; }
        public string paraBirimi { get; set; }
        public string tesisatNo { get; set; }
        public decimal? odenecek { get; set; }
        public decimal? vergi { get; set; }
        public string unvan { get; set; }
        public string gonderimSekli { get; set; }
        public int? firmaId { get; set; }
    }
}
