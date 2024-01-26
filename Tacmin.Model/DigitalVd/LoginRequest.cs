using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.DigitalVd
{
    public class LoginRequest
    {
        public string assoscmd { get; set; }
        public bool controlCaptcha { get; set; }
        public string dk { get; set; }
        public string imageId { get; set; }
        public string parola { get; set; }
        public string rtype { get; set; }
        public string sifre { get; set; }
        public string userid { get; set; }
    }
}
