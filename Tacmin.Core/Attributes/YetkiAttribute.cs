using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.WebPages;
using Tacmin.Core.Controllers;
using Tacmin.Core.Interface;

namespace Tacmin.Core
{
    [AttributeUsage(AttributeTargets.Method)]
    public class YetkiAttribute : AuthorizeAttribute
    {
        private readonly HttpRequestBase _req = Engine.Instance.Resolve<HttpRequestBase>();
        private readonly IVirtualContext _vctx = Engine.Instance.Resolve<IVirtualContext>();

        private string[] GerekenYetkiler { get; set; }

        public YetkiAttribute(params string[] yetkiler)
        {
            GerekenYetkiler = yetkiler;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!_vctx.IsLoged())
            {
                var token = _req.Cookies.Get("TOKEN");
                if (token != null && !token.Value.IsEmpty())
                    filterContext.HttpContext.Response.Redirect("/Login/GirisYap?token=" + token.Value + "&returl=" + HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.PathAndQuery));
                else
                    filterContext.HttpContext.Response.Redirect("/Login/CikisYap?returl=" + HttpUtility.UrlEncode(filterContext.HttpContext.Request.Url.PathAndQuery));

                base.HandleUnauthorizedRequest(filterContext);
            }
            else
            {
                var yetkiler = new List<string>();
                foreach (var y in GerekenYetkiler)
                {
                    yetkiler.Add(y);
                }

                var yetkili = _vctx.yetkiKontrol(yetkiler.ToArray());

                if (!yetkili)
                {
                    IController controller = DependencyResolver.Current.GetService<ErrorsController>();
                    var rt = new RouteData();
                    rt.Values["plugin"] = "core";
                    rt.Values["controller"] = "errors";
                    rt.Values["action"] = $"YetkisizErisim";
                    rt.Values["gerekenYetkiler"] = GerekenYetkiler;
                    var context = new RequestContext(filterContext.HttpContext, rt);

                    controller.Execute(context);
                }
            }
        }
    }
}
