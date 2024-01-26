using System;
using System.Net;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Tacmin.Core;
using Tacmin.Core.Infrastructure;
using Tacmin.Core.Mvc;

namespace TacminMusavir
{
    public class Global : TacminHttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            ServicePointManager.ServerCertificateValidationCallback += (a, b, c, d) => true;
            PreSendRequestHeaders += Application_PreSendRequestHeaders;

            Engine.Instance.RegisterAutofac();
            Engine.Instance.RunStartupTasks();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Engine.Config.OnConfigFileChanged += Config_OnConfigFileChanged;
            Engine.Config.OnConfigFileError += Config_OnConfigFileError;

            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new ValidatorProvider());
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}