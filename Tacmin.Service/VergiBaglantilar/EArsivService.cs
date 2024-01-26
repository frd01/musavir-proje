using Newtonsoft.Json;
using Tacmin.Model;

namespace Tacmin.Service.VergiBaglantilar
{
    public class EArsivService
    {
        private string login_url = "https://earsivportal.efatura.gov.tr/earsiv-services/assos-login";
        private string service_url = "";

        public string Giris(string gib_kodu, string gib_sifre)
        {

            var data = "assoscmd=anologin&rtype=json&userid=" + gib_kodu + "&sifre=" + gib_sifre + "&sifre2=" + gib_sifre + "&parola=1&";
            var response = GibRequestService.PostJsonRequest(login_url, data);
            var model = JsonConvert.DeserializeObject<GibLoginResultModel>(response);

            return model.Token;
        }
    }
}
