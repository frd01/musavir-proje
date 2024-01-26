using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.MaliyeIslemleri.Beyanname
{
    public class BeyannameOzetModel
    {
        public int BeyannameSayisi { get; set; }
        public int BeyannameTahakkuhSayisi { get; set; }
        public int BildirgeSayisi { get; set; }
        public int BildirgeTahakkuhSayisi { get; set; }
        public int MukellefSayisi { get; set; }
        public bool IslemDurum { get; set; }
        public string Mesaj { get; set; }
    }
}
