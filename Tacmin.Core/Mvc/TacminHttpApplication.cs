using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Tacmin.Core.Controllers;
using Tacmin.Core.Extensions;
using Tacmin.Core.Infrastructure;
using Tacmin.Core.Model;

namespace Tacmin.Core.Mvc
{
    public class TacminHttpApplication : HttpApplication
    {
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            if (!(sender is HttpApplication app) || app.Context == null)
                return;

            var headers = app.Context.Response.Headers;
            headers.Remove("Server");

            var cookies = app.Context.Response.Cookies;

            /*if (cookies.Exists(MvcUtils.GetSessionCookieName()))
            {
                if (AsEngine.Config.production)
                    cookies[MvcUtils.GetSessionCookieName()].Domain = "." + app.Context.Request.Url.Host;
            }*/
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            var newCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            var trculture = new CultureInfo("tr-TR");

            newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            newCulture.DateTimeFormat.FullDateTimePattern = "dd/MM/yyyy HH:mm:ss";
            newCulture.DateTimeFormat.DateSeparator = ".";
            newCulture.NumberFormat = trculture.NumberFormat;
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;
        }

        protected void Session_End(object sender, EventArgs e)
        {
            Engine.LoggedUsers.TryRemove(Session.SessionID, out _);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var httpContext = new HttpContextWrapper(Context);
            var exception = Server.GetLastError();
            var exceptions = exception.Messages();
            var code = (exception is HttpException) ? (exception as HttpException).GetHttpCode() : 500;
            var logusername = "application";
            var session = httpContext?.Session;
            dynamic usermodel = null;

            /*if (session != null)
                usermodel = session.Get<dynamic>("user");*/

            if (usermodel != null)
            {
                var sessionusername = usermodel?.DetailedUsername;
                if (sessionusername != null)
                    logusername = sessionusername;
            }
            else
            {
                /*var cookie = httpContext.Request.Cookies.Get(MvcUtils.GetSessionCookieName());

                if (cookie != null && Engine.LoggedUsers.TryGetValue(cookie.Value, out var user))
                    logusername = user.AdiSoyadi + " - " + user.Username;*/
            }

            var message = "--------------------------------------------------------\n";
            message += "Kullanıcı: " + logusername + " (" + Request.ServerVariables["REMOTE_ADDR"] + ")" + "\n";
            message += "Browser: " + Request.Headers["User-Agent"].ToString() + "\n";

            var errno = 0;
            foreach (var msg in exceptions)
            {
                message += $"Hata Mesajı {++errno}:" + msg + "\n\n";
            }

            var absoluteuri = Request.Url.AbsoluteUri;
            if (absoluteuri.ToLowerInvariant().Contains("token"))
            {
                var qs = HttpUtility.ParseQueryString(Request.Url.Query);
                if (!qs.Get("token").IsEmpty())
                    qs.Set("token", "HIDDENVALUE");

                message += absoluteuri.SafeSubstring(0, absoluteuri.IndexOf("?") + 1) + qs.ToString() + "\n";
            }
            else
            {
                message += absoluteuri + "\n";
            }

            if (Request.UrlReferrer != null)
            {
                var referrer = Request.UrlReferrer.AbsoluteUri;
                if (referrer.ToLowerInvariant().Contains("token"))
                {
                    var qs = HttpUtility.ParseQueryString(Request.UrlReferrer.Query);
                    if (!qs.Get("token").IsEmpty())
                        qs.Set("token", "HIDDENVALUE");

                    message += referrer.SafeSubstring(0, referrer.IndexOf("?") + 1) + qs.ToString() + "\n";
                }
                else
                {
                    message += "\nReferrer:" + referrer + "\n";
                }
            }

            message += "--------------------------------------------------------\n";

            if (code != 404)
            {
                message += "--------------------StackTrace--------------------------\n";
                message += exception.StackTrace + "\n";
                message += "--------------------------------------------------------\n";
            }

            message += "\n";
            NotifyDeveloper.Notify(message);

            var error = new ErrorViewModel
            {
                UserName = logusername,
                HttpErrorCode = code,
                Error = exception,
                ErrorMessages = exception.Messages().ToList().Distinct()
            };

            Response.Clear();
            Server.ClearError();

            if (code == 404 || code == 500)
            {
                if (!httpContext.Request.IsAjaxRequest())
                {
                    IController controller = DependencyResolver.Current.GetService<ErrorsController>();
                    var rt = new RouteData();
                    rt.Values["plugin"] = "core";
                    rt.Values["controller"] = "errors";
                    rt.Values["action"] = $"http{code}";
                    rt.Values["error"] = error;
                    var context = new RequestContext(httpContext, rt);
                    controller.Execute(context);
                }
                else
                {
                    Context.Response.ContentType = "application/json";
                    Context.Response.StatusCode = code;

                    var serializeSettings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                        ContractResolver = new DefaultContractResolver()
                    };

                    Context.Response.Write(JsonConvert.SerializeObject(error, serializeSettings));
                }
            }
        }

        protected void Config_OnConfigFileError(Exception e)
        {
            var message = $"Config değişimi sonrası hata. Eski confige geri dönülüyor.\nHata mesajı:" + e.Message;
            NotifyDeveloper.Notify(message);
        }

        protected void Config_OnConfigFileChanged(ConfigChangedEventArgs e)
        {
            NotifyDeveloper.Notify($"Config değişti.");

            try
            {
                Engine.Instance.RunStartupTasks();
            }
            catch (Exception ex)
            {
                NotifyDeveloper.Notify($"Config değişimi sonrası startuptasklarda hata. Eski confige geri dönülüyor.\nHata mesajı:" + ex.Message + "\nStackTrace: " + ex.StackTrace);
                e.Cancel = true;
            }
        }
    }
}
