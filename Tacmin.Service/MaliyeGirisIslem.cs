using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebPages;
using System.Xml;
using Tacmin.Model;
using Tesseract;

namespace Tacmin.Service
{
    public class MaliyeGirisIslem
    {
        private readonly string IVD_LOGIN_URL = "https://intvrg.gib.gov.tr/intvrg_server/assos-login";
        private readonly string E_BEYANNAME_LOGIN_URL = "https://ebeyanname.gib.gov.tr/eyeks?_dc=1535709090048";
        private readonly string E_ARSIV_PORTAL_URL = "https://earsivportal.efatura.gov.tr/earsiv-services/assos-login";
        private ServiceIslem service;

        public MaliyeGirisIslem(ServiceIslem _service)
        {
            service = _service;
        }

        private string GetIvdCaptcha(string imageID)
        {
            var lxRequest = (HttpWebRequest)WebRequest.Create("https://intvrg.gib.gov.tr/captcha/jcaptcha?imageID=" + imageID);
            lxRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";

            var lxResponse = (HttpWebResponse)lxRequest.GetResponse();
            var binReader = new BinaryReader(lxResponse.GetResponseStream());
            //var lnByte = binReader.ReadBytes(1 * 1024 * 1024 * 10);
            var lnByte = service.GetFileByte("https://intvrg.gib.gov.tr/captcha/jcaptcha?imageID=" + imageID).Result;

            var dosya = new TesseractEngine(HttpContext.Current.Server.MapPath(@"~\assets\lib\tessdata\"), "eng");
            var letters = "abcdefghijklmnopqrstuvwxyz";
            var numbers = "0123456789";
            dosya.SetVariable("tessedit_char_whitelist", $"{numbers}{letters}{letters.ToUpper()}");
            dosya.SetVariable("tessedit_unrej_any_wd", true);
            dosya.SetVariable("tessedit_adapt_to_char_fragments", true);
            dosya.SetVariable("tessedit_redo_xheight", true);
            dosya.SetVariable("chop_enable", true);

            var memoryStream = new MemoryStream();
            memoryStream.Write(lnByte, 0, lnByte.Length);
            var bmp = (Bitmap)Bitmap.FromStream(memoryStream);
            var img = PixConverter.ToPix(bmp);
            var txt = dosya.Process(img);
            return txt.GetText().Replace("\n", "").Replace(" ", "").ToUpper();
        }

        public GibLoginResultModel IvdGirisYap(string userId, string password)
        {
            var captchaSayac = 0;
            var captcha = "";
            var imageID = "";
            while (captcha.Length != 6 && captchaSayac < 10)
            {
                var random = new Random();
                const string chars = "abcdefghijklmnoprstuvyz0123456789";
                imageID = new string(Enumerable.Repeat(chars, 16).Select(s => s[random.Next(s.Length)]).ToArray());
                captcha = GetIvdCaptcha(imageID);

                captchaSayac += 1;
            }

            var jSonData = new GibLoginResultModel();

            var data = "assoscmd=multilogin&rtype=json&userid=" + userId + "&sifre=" + password + "&parola=maliye&dk=" + captcha + "&controlCaptcha=true&imageID=" + imageID;
            var result = service.PostJsonRequest(IVD_LOGIN_URL, data);

            jSonData = JsonConvert.DeserializeObject<GibLoginResultModel>(result);
            return jSonData;
        }

        private GibLoginResultModel TryIvdLogin(string userId, string password)
        {
            var captchaSayac = 0;
            var captcha = "";
            var imageID = "";
            while (captcha.Length != 6 && captchaSayac < 10)
            {
                var random = new Random();
                const string chars = "abcdefghijklmnoprstuvyz0123456789";
                imageID = new string(Enumerable.Repeat(chars, 16).Select(s => s[random.Next(s.Length)]).ToArray());
                captcha = GetIvdCaptcha(imageID);

                captchaSayac += 1;
            }

            var jSonData = new GibLoginResultModel();

            var data = "assoscmd=multilogin&rtype=json&userid=" + userId + "&sifre=" + password + "&parola=maliye&dk=" + captcha + "&controlCaptcha=true&imageID=" + imageID;
            var result = service.PostJsonRequest(IVD_LOGIN_URL, data);

            if (result.Contains("Doğrulama hatası"))
            {
                return jSonData;
            }

            jSonData = JsonConvert.DeserializeObject<GibLoginResultModel>(result);
            return jSonData;
        }

        public GibLoginResultModel IvdLogin(string userid, string password)
        {
            var response = new GibLoginResultModel();
            for (var i = 0; i < 25; i++)
            {
                response = TryIvdLogin(userid, password);

                if (!response.Token.IsEmpty() || response.messages != null)
                {
                    break;
                }
            }

            return response;
        }

        public string IvdGeciciTokenUret()
        {
            var data = "assoscmd=cfsession&rtype=json&fskey=intvrg.fix.session&fuserid=INTVRG_FIX&";
            var result = GibRequestService.PostJsonRequest(IVD_LOGIN_URL, data);

            var jSonData = JsonConvert.DeserializeObject<GibLoginResultModel>(result);
            return jSonData.Token;
        }

        public string EskiEbeyannameToken(string userid, string parola, string password)
        {
            var data = "username=" + userid + "&password2=" + parola + "&password1=" + password + "&eyekscommand=ajaxlogin&redirectionpath=context1";
            var response = GibRequestService.PostJsonRequest(E_BEYANNAME_LOGIN_URL, data);
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(response.ToString());

            var token = "";
            foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
            {
                if ((node.NodeType == XmlNodeType.Element) && (node.Name == "TOKEN"))
                {
                    token = node.InnerText;
                }
            }

            return token;
        }

        public void IvdCikisYap(string token)
        {
            var data = "cmd=kullaniciBilgileriService_logout&" +
                "callid=924e08bceb714-21&" +
                "pageName=PG_MAIN_DYNAMIC&" +
                "token=" + token + "&jp=%7B%7D";
            service.PostJsonRequest("https://intvrg.gib.gov.tr/intvrg_server/dispatch", data);
        }

        public string GibEarsivPortalGiris(string Userid, string Password)
        {
            var data = "assoscmd=anologin&rtype=json&userid=" + Userid + "&sifre=" + Password + "&sifre2=" + Password + "&parola=1&";
            var response = GibRequestService.PostJsonRequest(E_ARSIV_PORTAL_URL, data);
            var model = JsonConvert.DeserializeObject<GibLoginResultModel>(response);

            return model.Token;
        }

        public void GibEarsivPortalCikis(string Token)
        {
            var data = "assoscmd=logout&rtype=json&token=" + Token + "&";
            GibRequestService.PostJsonRequest(E_ARSIV_PORTAL_URL, data);
        }
    }
}
