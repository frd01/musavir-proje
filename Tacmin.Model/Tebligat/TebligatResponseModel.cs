using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Model.Gib;

namespace Tacmin.Model.Tebligat
{
    public class TebligatResponseModel
    {
        public List<TebligatDataListModel> TebligatListesi { get; set; }
        public List<GibTabloSonuc> HataListesi { get; set; }
        public string IslemDurum { get; set; }
        public string Token { get; set; }
        public string DataStr { get; set; }
    }
}
