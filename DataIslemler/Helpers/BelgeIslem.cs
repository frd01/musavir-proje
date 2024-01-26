using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIslemler.Helpers
{
    public class BelgeIslem
    {
        private readonly string local_base_url = "http://localhost:62457/";
        private readonly string server_base_url = "https://test.emusavirim.com/";

        public string GetBaseUrl
        {
            get
            {
                return server_base_url;
            }
        }
    }
}
