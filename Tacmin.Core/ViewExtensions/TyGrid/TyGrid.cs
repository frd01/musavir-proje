using Kendo.Mvc;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Html;
using System.Web.Mvc;

namespace Tacmin.Core.ViewExtensions
{
    public class TyGrid<T> : Grid<T> where T : class
    {
        public TyGrid(
            ViewContext viewContext,
            IJavaScriptInitializer initializer,
            IUrlGenerator urlGenerator,
            IGridHtmlBuilderFactory htmlBuilderFactory
            ) : base(viewContext, initializer, urlGenerator, htmlBuilderFactory)
        {
        }
    }
}
