using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Banka
{
    public class BankaResponseModel<R, P>
    {
        public List<R> RaporListe { get; set; }
        public List<P> ParametreListesi { get; set; }

    }
}
