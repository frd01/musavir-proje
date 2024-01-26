using System.Web.Optimization;

namespace TacminMusavir
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/main/js").Include(
                "~/assets/lib/jquery-3.6.0.min.js",
                "~/assets/lib/bootstrap/js/bootstrap.bundle.min.js",
                "~/assets/lib/adminlte/js/adminlte.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/kendo/js").Include(
                "~/assets/lib/kendo/js/jszip.min.js",
                "~/assets/lib/kendo/js/kendo.all.min.js",
                "~/assets/lib/kendo/js/kendo.aspnetmvc.min.js",
                "~/assets/lib/kendo/js/messages/kendo.messages.tr-TR.min.js",
                "~/assets/lib/kendo/js/cultures/kendo.culture.tr-TR.min.js"
                ));

            var kendoStyles = new StyleBundle("~/bundles/kendo/css")
                .Include("~/assets/lib/kendo/css/kendo.common.min.css", new CssRewriteUrlTransform())
                .Include("~/assets/lib/kendo/css/kendo.fiori.min.css", new CssRewriteUrlTransform())
                .Include("~/assets/lib/kendo/css/kendo.fiori.mobile.min.css", new CssRewriteUrlTransform()
                );

            bundles.Add(kendoStyles);

            bundles.Add(new ScriptBundle("~/bundles/libs/js").Include(
                "~/assets/lib/node_modules/toastr/build/toastr.min.js",
                "~/assets/lib/node_modules/inputmask/jquery.inputmask.min.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/libs/css").Include(
                "~/assets/lib/node_modules/toastr/build/toastr.min.css"
                ));

            bundles.Add(new StyleBundle("~/bundles/main/css").Include(
                "~/assets/lib/adminlte/css/adminlte.min.css"
                ));

            bundles.Add(new StyleBundle("~/bundles/tacmin/css").Include(
                "~/assets/lib/tacmin/style.min.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/tacmin/js").Include(
                "~/assets/lib/tacmin/init.min.js"
                ));
        }
    }
}