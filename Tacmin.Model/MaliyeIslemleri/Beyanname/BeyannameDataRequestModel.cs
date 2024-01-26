using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.MaliyeIslemleri.Beyanname
{
    public class BeyannameDataRequestModel
    {
        public List<int> Cariler { get; set; }
        public List<string> BeyannameTurler { get; set; }

        public DateTime? IlkTarih { get; set; }
        public DateTime? SonTarih { get; set; }
    }
}
