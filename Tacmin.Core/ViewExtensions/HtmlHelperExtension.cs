using System.Web.Mvc;

namespace Tacmin.Core.ViewExtensions
{
    public static class HtmlHelperExtension
    {
        public static TyWidgetFactory<TModel> Ty<TModel>(this HtmlHelper<TModel> helper) where TModel : class
        {
            return new TyWidgetFactory<TModel>(helper);
        }
    }
}
