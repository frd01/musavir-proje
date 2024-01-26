using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model
{
    public class RaporParamsModel<R, P>
    {
        public List<R> RaporListe { get; set; }
        public List<P> ParamsListe { get; set; }
    }
}
