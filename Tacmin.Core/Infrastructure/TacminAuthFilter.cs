using Newtonsoft.Json;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using Tacmin.Core.Interface;

namespace Tacmin.Core.Infrastructure
{
    public class TacminAuthFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var res = Engine.Instance.TryResolve<IVirtualContext>(out var ctx);
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLowerInvariant();

            var bypass = false;
            if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                bypass = true;
            else if (controllerName == "login")
                bypass = true;

            Engine.Instance.TryResolve<IAuthService>(out var _auth);
            Engine.Instance.TryResolve<IEncryptionService>(out var _enc);
            var remembercookiename = $"{Engine.Settings.oturum_remember_cookie_adi}_{HttpContext.Current.Request.Url.Port}";

            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[remembercookiename] != null && _auth != null && _enc != null)
            {
                if (HttpContext.Current.Request.Cookies[remembercookiename].Value == "1")
                {
                    var oturumCookie = $"{Engine.Settings.oturum_cookie_adi}_{HttpContext.Current.Request.Url.Port}";

                    if (HttpContext.Current.Request.Cookies[oturumCookie] != null)
                    {
                        var oturumCookieVal = HttpContext.Current.Request.Cookies[oturumCookie].Value;

                        var identity = JsonConvert.DeserializeObject<dynamic>(_enc.Decrypt(oturumCookieVal));
                        if (identity != null && identity.Username != null && identity.Md5Password != null)
                        {
                            var isvalid = _auth.IsValidAuth(identity.Usercode.Value, identity.Username.Value, identity.Md5Password.Value);
                            if (isvalid)
                            {
                                _auth.SignIn(identity.Usercode.Value, identity.Username.Value, true);
                            }
                        }
                    }
                }
            }

            if (!bypass && (!res || !ctx.IsLoged()))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                         { "controller", "Login" },
                         { "action", "GirisYap" }
                    }
                );
            }
        }
    }
}
