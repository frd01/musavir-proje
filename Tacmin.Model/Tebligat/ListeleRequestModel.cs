using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Model.Mukellef;

namespace Tacmin.Model.Tebligat
{
    public class ListeleRequestModel
    {
        public List<FirmaGibModel> FirmaListesi { get; set; }
        public bool? isOkundu { get; set; }
        public bool? isOkunmadi { get; set; }
        public bool? isHepsi { get; set; }
    }
}
