using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model
{
    public class IslemResponse<T,X>
    {
        public bool IslemDurum { get; set; }
        public string Mesaj { get; set; }
        public T Data { get; set; }
        public X DigerBilgiler { get; set; }
    }
}
