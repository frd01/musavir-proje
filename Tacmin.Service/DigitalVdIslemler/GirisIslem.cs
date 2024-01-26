using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebPages;
using Tacmin.Model;
using Tacmin.Model.DigitalVd;
using Tesseract;

namespace Tacmin.Service.DigitalVdIslemler
{
    public class GirisIslem
    {
        private readonly string IVD_LOGIN_URL = "https://dijital.gib.gov.tr/apigateway/auth/tdvd/login";

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


            var model = new LoginRequest();

            model.assoscmd = "multilogin";
            model.controlCaptcha = true;
            model.dk = captcha;
            model.imageId = imageID;
            model.parola = "maliye";
            model.rtype = "json";
            model.sifre = password;
            model.userid = userId;

            var headers = new Dictionary<string, string>();

            headers.Add("Content-Type", "application/json");

            var result = new ServiceIslem().PostMethod<GibLoginResultModel>(model, IVD_LOGIN_URL, headers);




            if (result.messages!=null)
            {
                if (result.messages[0].text == "Doğrulama kodunu girmek için süre sınırı aşıldı.")
                {
                    return result;
                }
            }

           
            return result;
        }

        private string GetIvdCaptcha(string imageID)
        {
            //var lxRequest = (HttpWebRequest)WebRequest.Create("https://dijital.gib.gov.tr/apigateway/captcha/getnewcaptcha?cid=" + imageID);
            //lxRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.106 Safari/537.36";

            var url = "https://dijital.gib.gov.tr/apigateway/captcha/getnewcaptcha?cid=" + imageID;

            var result = new ServiceIslem().GetMethod<CapctModel>(url);

            //var lxResponse = (HttpWebResponse)lxRequest.GetResponse();

            var fileByte = Convert.FromBase64String(result.captchaImgBase64);

            var stream = new MemoryStream(fileByte);
            //var binReader = new BinaryReader(lxResponse.GetResponseStream());
            var binReader = new BinaryReader(stream);
            var lnByte = binReader.ReadBytes(1 * 1024 * 1024 * 10);

            // https://dijital.gib.gov.tr/apigateway/captcha/getnewcaptcha?cid=435015a8-75be-4e5f-82a2-2ea95f869ac0

            // https://intvrg.gib.gov.tr/captcha/jcaptcha?imageID=435015a8-75be-4e5f-82a2-2ea95f869ac0

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
    }
}
