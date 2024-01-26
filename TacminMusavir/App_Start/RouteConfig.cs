using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Tacmin.Core;
using Tacmin.Core.Extensions;

namespace TacminMusavir
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.LowercaseUrls = true;

            routes.MapRoute(
               name: $"TacminMusavir",
               url: "{controller}/{action}",
               defaults: new { plugin = "default", controller = "main", action = "index" },
               namespaces: new string[] { "TacminMusavir.Controllers" }
            );

            var namespaces = Engine.Instance.Assemblies.SelectMany(m => m.GetTypes().Where(type => typeof(Controller).IsAssignableFrom(type)))
                .Where(m => m.Namespace.Split(".").Length == 3)
                .Select(m => m.Namespace)
                .Distinct()
                .Select(m => new
                {
                    PluginName = m.Split(".")[1].ToLowerInvariant(),
                    Namespace = m
                });

            foreach (var item in namespaces)
            {
                var route = routes.MapRoute(
                    name: $"{item.PluginName}",
                    url: $"{item.PluginName}/{{controller}}/{{action}}/{{id}}",
                    defaults: new { plugin = item.PluginName, controller = "Main", action = "Index", id = UrlParameter.Optional },
                    namespaces: new string[] { item.Namespace }
                );
            }

        }
    }
}