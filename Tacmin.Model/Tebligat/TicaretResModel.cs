using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Model.Gib;

namespace Tacmin.Model.Tebligat
{
    public class TicaretResModel
    {
        public List<TebligatDataListModel> TebligatListesi { get; set; }
        public List<GibTabloSonuc> HataListesi { get; set; }
    }
}
