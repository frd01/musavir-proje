using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tacmin.Core;
using Tacmin.Core.Extensions;
using Tacmin.Core.Interface;
using Tacmin.Data;
using Tacmin.Data.DbModel;
using Tacmin.Data.Repositories;
using Tacmin.Model.Login;

namespace Tacmin.Service.Authentication
{
    public class AuthService : IAuthService
    {
        public const string KULLANICI_DEGISTIREBILIR_SESSION_ADI = nameof(KULLANICI_DEGISTIREBILIR_SESSION_ADI);
        public const string GERCEK_KULLANICI_COOKIE = nameof(GERCEK_KULLANICI_COOKIE);

        private readonly MainRepository<KULLANICI_TANIMLARI> _userRep;
        private readonly MainContext _mctx;
        private readonly HttpSessionStateBase _session;
        private readonly HttpResponseBase _response;
        private readonly IVirtualContext _vctx;
        private readonly HttpRequestBase _request;
        private readonly EncryptionService _enc;

        public AuthService(
            MainRepository<KULLANICI_TANIMLARI> userRep,
            MainContext mctx,
            HttpSessionStateBase session,
            HttpResponseBase response,
            IVirtualContext vctx,
            HttpRequestBase request,
            EncryptionService enc
            )
        {
            _userRep = userRep;
            _mctx = mctx;
            _session = session;
            _response = response;
            _vctx = vctx;
            _request = request;
            _enc = enc;
        }

        public bool IsValidAuth(string kullaniciKodu, string kullaniciAdi, string sifreMd5)
        {
            KULLANICI_TANIMLARI kullanici = null;

            var data_model = new DataIslemler.Islemler.KullaniciIslem("Yonetim_Musavir_Db");

            //kullanici = _userRep.Get(x => x.AKTIF == true && x.KULLANICI_KODU == kullaniciKodu && x.SIFRE == sifreMd5);

            kullanici = data_model.GetDataKullaniciModel(kullaniciKodu, sifreMd5);

            if (kullanici == null) 
                return false;

            return true;
        }

        public void SignIn(string kullaniciKodu, string kullaniciAdi, bool hatirla)
        {
            try
            {
                var user = GetUserModel(kullaniciKodu, kullaniciAdi);

                var data_cookie = new HttpCookie("data_adi", user.DataAdi.ToString());
                data_cookie.Expires = DateTime.Now.AddDays(365);

                _response.Cookies.Set(data_cookie);

                var kullaniciDegistirebilir = user.AltKullaniciIds.Count() > 0 || user.Yetkili;

                _session.Set(KULLANICI_DEGISTIREBILIR_SESSION_ADI, kullaniciDegistirebilir);
                _session.Set("UserModel", user);

                Engine.LoggedUsers[_session.SessionID] = user;

                var cookiename = $"{Engine.Settings.oturum_cookie_adi}_{_request.Url.Port}";

                var cookie = new HttpCookie(cookiename, user.Token.ToString())
                {
                    Expires = DateTime.Now.AddHours(Engine.Settings.oturum_timeout_suresi)
                };

                if (hatirla)
                {
                    cookie.Expires = DateTime.Now.AddDays(365);
                    var remembercookiename = $"{Engine.Settings.oturum_remember_cookie_adi}_{_request.Url.Port}";

                    var remembercookie = new HttpCookie(remembercookiename, "1")
                    {
                        Expires = DateTime.Now.AddDays(365)
                    };

                    _response.Cookies.Set(remembercookie);
                }
                else
                {
                    var remembercookiename = $"{Engine.Settings.oturum_remember_cookie_adi}_{_request.Url.Port}";

                    var remembercookie = new HttpCookie(remembercookiename, "0")
                    {
                        Expires = DateTime.Now.AddSeconds(-1)
                    };

                    _response.Cookies.Set(remembercookie);
                }

                _response.Cookies.Set(cookie);

                //return "hatasız çalıştı";
            }
            catch (Exception ex)
            {

                Console.WriteLine("Sig in method hata : ",ex.Message);

                //return ex.Message;
            }
        } 



        public UserModel GetUserModel(string kullaniciKodu, string kullaniciAdi)
        {
            //var kullanici = _userRep.Get(x => x.KULLANICI_KODU == kullaniciKodu); 

            var kullanici = new DataIslemler.Islemler.KullaniciIslem("Yonetim_Musavir_Db").GetKullaniciModel(kullaniciKodu); 

            if (kullanici == null)
                return null;

            var user = new UserModel 
            {
                UserId = kullanici.Id,
                Yetkili = kullanici.Yetkili == true,
                KullaniciAdi = "",
                Adi = kullanici.Adi,
                Soyadi = kullanici.SoyAdi,
                AltKullaniciIds = new List<int>(),
                Yetkiler = kullanici.Yetkiler.Split(","),
                KullaniciKodu = kullaniciKodu,
                GibKodu = kullanici.GibKodu,
                GibSifre = kullanici.GibSifre,
                NtbKodu = kullanici.NtbKodu,
                NtbSifre = kullanici.NtbSifre,
                DataAdi = kullanici.DataAdi
            };

            var identity = new
            {
                Usercode = user.KullaniciKodu,
                Username = user.KullaniciAdi,
                Md5Password = kullanici.Sifre
            };

            var rawvalue = JsonConvert.SerializeObject(identity);

            var serialize = _enc.Encrypt(rawvalue);

            user.Token = serialize;

            return user;
        }
    }
}
