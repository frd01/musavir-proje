using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using System;
using System.Web.Mvc;

namespace Tacmin.Core.ViewExtensions
{
    public class TyToolBarBuilder : ToolBarBuilder
    {
        private HtmlHelper _htmlHelper;
        public TyToolBar TyToolbarComponent => ((TyToolBar)Component);

        public TyToolBarBuilder(TyToolBar component, HtmlHelper htmlHelper) : base(component)
        {
            _htmlHelper = htmlHelper;

            Items(items =>
            {
                items.Add().Template($"<label class='mb-0 pb-0 pl-2 text-md'>{_htmlHelper.ViewBag.Title}</label>");
                items.Add().Type(CommandType.Separator);
                items.Add().Type(CommandType.Spacer);
            });
        }

        public TyToolBarBuilder Items(Action<TyToolBarItemFactory> configurator)
        {
            configurator(new TyToolBarItemFactory(Component.Items));
            return this;
        }
    }
}
