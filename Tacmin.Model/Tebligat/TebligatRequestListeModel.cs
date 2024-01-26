using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Tebligat
{
    public class TebligatRequestListeModel
    {
        public TebligatParameters parametreler { get; set; }
        public string respKeyParam { get; set; }
        public TebligatPm pv { get; set; }
    }

    public class TebligatParameters
    {
        public string duzBirim { get; set; }
        public string duzBasZaman { get; set; }
        public string duzBitZaman { get; set; }
        public string tebligBasZaman { get; set; }
        public string tebligBitZaman { get; set; }
        public List<string> durum { get; set; }
        public string belgeTuru { get; set; }
        public string belgeNo { get; set; }
        public bool paging { get; set; }
    }

    public class TebligatPm
    {
        public int start { get; set; }
        public string limit { get; set; }
        public List<object> sorters { get; set; }
    }
}
