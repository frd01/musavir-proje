using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Gib
{
    public class GibLoginResponseModel
    {
        public string token { get; set; }
        public bool? chgpwd { get; set; }
        public string redirectUrl { get; set; }
        public string error { get; set; }
        public List<GibMesajModel> messages { get; set; }
    }

    public class GibMesajModel
    {

        public string type { get; set; }
        public string text { get; set; }
    }
}
