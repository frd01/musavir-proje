using Newtonsoft.Json;
using Tacmin.Model;

namespace Tacmin.Service.VergiBaglantilar
{
    public class GibService
    {
        private string ivd_login_url = "https://ivd.gib.gov.tr/tvd_server/assos-login";
        private string ivd_dispatch_url = "https://ivd.gib.gov.tr/tvd_server/dispatch";

        private GibService gibService;

        public GibLoginResultModel GibGiris(string gib_kodu, string gib_sifre, string gib_parola)
        {


            var data_str = "assoscmd=multilogin&rtype=json&userid=" + gib_kodu + "&sifre=" + gib_sifre + "&parola=" + gib_parola + "&dk=A66NH1&controlCaptcha=false&imageID=35q6aylb6ef2th00&";

            var result = GibRequestService.PostJsonRequest(ivd_login_url, data_str);

            var jSonData = new GibLoginResultModel();

            if (result.Contains("Doğrulama hatası"))
            {
                return jSonData;
            }

            jSonData = JsonConvert.DeserializeObject<GibLoginResultModel>(result);
            return jSonData;
        }




    }


}
