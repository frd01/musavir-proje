using Kendo.Mvc;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using System.Web.Mvc;

namespace Tacmin.Core.ViewExtensions
{
    public class TyFilter<T> : Filter<T>
    {
        public TyFilter(ViewContext viewContext, IJavaScriptInitializer initializer, IUrlGenerator urlGenerator) : base(viewContext, initializer, urlGenerator)
        {
        }
    }
}
