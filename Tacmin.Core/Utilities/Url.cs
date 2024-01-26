using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Tacmin.Core.Utilities
{
    public static class Url
    {
        public static string Action(string actionName, string controllerName)
        {
            return new UrlHelper(HttpContext.Current.Request.RequestContext).Action(actionName, controllerName);
        }

        public static string Action(string actionName, string controllerName, object routeValues)
        {
            return new UrlHelper().Action(actionName, controllerName, routeValues);
        }

        public static string Action(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            return new UrlHelper().Action(actionName, controllerName, routeValues);
        }

        public static string RouteUrl(string routeName)
        {
            return new UrlHelper().RouteUrl(routeName);
        }

        public static string RouteUrl(object routeValues)
        {
            return new UrlHelper(HttpContext.Current.Request.RequestContext).RouteUrl(routeValues);
        }

        public static string RouteUrl(string routeName, object routeValues)
        {
            return new UrlHelper().RouteUrl(routeName, routeValues);
        }

        public static string RouteUrl(string routeName, RouteValueDictionary routeValues)
        {
            return new UrlHelper().RouteUrl(routeName, routeValues);
        }
    }
}

