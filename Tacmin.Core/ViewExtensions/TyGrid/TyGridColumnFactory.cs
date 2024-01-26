using Kendo.Mvc;
using Kendo.Mvc.UI.Fluent;
using System.Web;
using System.Web.Mvc;

namespace Tacmin.Core.ViewExtensions
{
    public class TyGridColumnFactory<TModel> : GridColumnFactory<TModel> where TModel : class
    {
        private readonly TyGrid<TModel> _container;
        private readonly ViewContext _viewContext;
        private readonly IUrlGenerator _urlGenerator;

        public TyGridColumnFactory(
            TyGrid<TModel> container,
            ViewContext viewContext,
            IUrlGenerator urlGenerator
            ) : base(container, viewContext, urlGenerator)
        {
            _container = container;
            _viewContext = viewContext;
            _urlGenerator = urlGenerator;
        }

        public TyGridColumnFactory<TModel> Detay(string route, string target = "_self", string icon = "fas fa-chevron-right",int width=50)
        {
            var html_a_tag = "<button class='btn' onclick='link_ac_event("+"'deneme'"+")' ><i class='" + icon + "'</i></button>";
            this.Template(tmp => "")
                .Width(width)
                .HtmlAttributes(new { style = "margin-left : 20px;"  })
                .ClientTemplate($"<a id='a_tag' href='{HttpUtility.UrlDecode(route)}' target='{target}'><i class='{icon}'></i></a>")
                //.ClientTemplate(html_a_tag)
                ;
            return this;
        }
    }
}
