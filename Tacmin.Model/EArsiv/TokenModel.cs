using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.EArsiv
{
    public class TokenModel
    {
        public string redirectUrl { get; set; }
        public string token { get; set; }
        public string error { get; set; }
        public List<TokenModelMesaj> messages { get; set; }
    }

    public class TokenModelMesaj
    {

        public string type { get; set; }
        public string text { get; set; }
    }
}
