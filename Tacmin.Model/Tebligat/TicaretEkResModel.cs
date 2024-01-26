using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Tebligat
{
    public class TicaretEkResModel_Data
    {
        public List<TicaretEkResModel> data { get; set; }
    }
    public class TicaretEkResModel
    {
        public string oid { get; set; }
        public string gonderilenDosyaAdi { get; set; }
    }
}
