using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIslemler.Helpers
{
    public class UrlIslem
    {
        private string localBaseUrl = "http://localhost:62457/";
        private string serverBaseUrl = "https://test.emusavirim.com/";

        private string baseUrl = "";

        public UrlIslem()
        {

            baseUrl = serverBaseUrl;
        }

        public string BaseUrl
        {
            get
            {
                return baseUrl;
            }
        }
    }
}
