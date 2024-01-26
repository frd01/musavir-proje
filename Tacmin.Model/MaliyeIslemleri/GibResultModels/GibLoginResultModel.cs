using System.Collections.Generic;

namespace Tacmin.Model
{
    public class GibLoginResultModel
    {
        public string Token { get; set; }
        public string RedirectUrl { get; set; }
        public List<GibLoginResultModelMessage> messages { get; set; }

    }

    public class GibLoginResultModelMessage
    {

        public string type { get; set; }
        public string text { get; set; }
    }
}
