using Kendo.Mvc;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using Kendo.Mvc.UI.Html;
using System.Web.Mvc;
using Tacmin.Core.Utilities;

namespace Tacmin.Core.ViewExtensions
{
    public class TyWidgetFactory<TModel> : WidgetBase
    {
        private readonly ViewContext _viewContext;
        private readonly HtmlHelper _htmlHelper;

        public TyWidgetFactory(HtmlHelper htmlHelper) : base(htmlHelper.ViewContext)
        {
            _viewContext = htmlHelper.ViewContext;
            _htmlHelper = htmlHelper;
        }

        public TyToolBarBuilder TyToolBar()
        {
            return new TyToolBarBuilder(new TyToolBar(_viewContext, DI.Current.Resolve<IJavaScriptInitializer>(), DI.Current.Resolve<IUrlGenerator>()), _htmlHelper);
        }

        public TyGridBuilder<T> TyGrid<T>() where T : class
        {
            return new TyGridBuilder<T>(new TyGrid<T>(_viewContext, DI.Current.Resolve<IJavaScriptInitializer>(), DI.Current.Resolve<IUrlGenerator>(), DI.Current.Resolve<IGridHtmlBuilderFactory>()));
        }

        public TyFilterBuilder<T> TyFilter<T>() where T : class
        {
            return new TyFilterBuilder<T>(new TyFilter<T>(_viewContext, DI.Current.Resolve<IJavaScriptInitializer>(), DI.Current.Resolve<IUrlGenerator>()));
        }

        public DropDownTreeBuilder MukellefListDropDownTree(string name)
        {
            var ddt = new DropDownTree(_viewContext, DI.Current.Resolve<IJavaScriptInitializer>(), DI.Current.Resolve<IUrlGenerator>(), DI.Current.Resolve<INavigationItemAuthorization>());

            var ddtbuilder = new DropDownTreeBuilder(ddt);
            ddtbuilder.Name(name)
                    .DataSource(ds => ds.Read(r => r.Url(Url.RouteUrl(new { controller = "hesap", action = "mukellefListesi" }))))
                    .DataValueField("Value")
                    .DataTextField("Text")
                    .Checkboxes(true)
                    .AutoWidth(true)
                    .CheckAll(true)
                    .CheckAllTemplate("Tümü")
                    .TagMode(TagMode.Single)
                    .Messages(msg => msg.SingleTag("mükellef seçildi"))
                    .AutoClose(false);

            return ddtbuilder;
        }
    }
}
