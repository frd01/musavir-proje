using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Model.EArsiv;

namespace Tacmin.Service.Sorgulamalar.Girisler
{
    public class InteraktifVd
    {
        private ServiceIslem service;

        public InteraktifVd(ServiceIslem _service)
        {

            service = _service;
        }
        public TokenModel GirisIslem(string gibKodu, string gibSifre, string gibParola)
        {
            var data_str = "assoscmd=multilogin&rtype=json&userid=" + gibKodu + "&sifre=" + gibSifre + "&parola=" + gibParola + "&dk=A66NH1&controlCaptcha=false&imageID=35q6aylb6ef2th00&";

            var servisUrl = "https://ivd.gib.gov.tr/tvd_server/assos-login";

            var response = service.PostJsonRequest(servisUrl, data_str);

            var result = JsonConvert.DeserializeObject<TokenModel>(response);

            return result;

        }

        public void CikisIslem(string token)
        {
            var data_str = "callid=39a2685feaf1c-23&cmd=kullaniciBilgileriService_logout&jp={}&pageName=PG_MAIN_DYNAMIC&token=" + token;

            var url = "https://ivd.gib.gov.tr/tvd_server/dispatch";

            var response = service.PostJsonRequest(url, data_str);

            Console.WriteLine($"İnteraktif çıkış {response}");

        }
    }
}
