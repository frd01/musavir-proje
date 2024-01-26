using System;
using System.Web;
using System.Web.Mvc;
using Tacmin.Core;
using Tacmin.Core.Extensions;
using Tacmin.Core.Model;
using Tacmin.Model;
using Tacmin.Service.Authentication;

namespace TacminMusavir.Controllers
{
    public class LoginController : BaseController
    {
        private readonly AuthService _authService;

        public LoginController(
            AuthService authService
            )
        {
            _authService = authService;
        }

        public ActionResult GirisYap(string token, string returl)
        {
            if (!token.IsEmpty())
            {
                var benihatirlacookie = Request.Cookies.Get("BENI_HATIRLA");
                var benihatirla = false;
                if (benihatirlacookie != null)
                    benihatirla = benihatirlacookie.Value.ToBool();
            }

            return View();
        }

        [HttpPost]
        public ActionResult Giris(UserLoginModel usermodel)
        {
            if (ModelState.IsValid)
            {
                var isvalid = _authService.IsValidAuth(usermodel.kullaniciKodu, usermodel.kullaniciAdi, usermodel.sifre.Md5());
                if (isvalid)
                {
                    _authService.SignIn(usermodel.kullaniciKodu, usermodel.kullaniciAdi, usermodel.beniHatirla);
                    if (!usermodel.returl.IsEmpty())
                        return Redirect(HttpUtility.UrlDecode(usermodel.returl));
                    else
                        return RedirectToAction("Index", "Main");
                }
            }

            //Toast("Giriş başarısız. Bilgilerinizi kontrol edip tekrar deneyiniz.", ToastrType.Error);
            //return RedirectToAction("GirisYap", "Login");
            return View();
        }

        [HttpPost]
        public ActionResult KullaniciGiris(UserLoginModel usermodel)
        {
            var isvalid = _authService.IsValidAuth(usermodel.kullaniciKodu, usermodel.kullaniciAdi, usermodel.sifre.Md5());
            if (isvalid)
            {
                Console.WriteLine("Sig in method hata : ");
                _authService.SignIn(usermodel.kullaniciKodu, usermodel.kullaniciAdi, usermodel.beniHatirla);
                return Json(isvalid);
                //return Json(isvalid);
            }
            return Json(false);
        }

        public ActionResult CikisYap(string returl)
        {
            Session.Clear();
            return Redirect(Url.RouteUrl(new { action = "GirisYap", controller = "Login", returl }));
        }
    }
}