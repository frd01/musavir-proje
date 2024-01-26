using System.Web.Mvc;
using Tacmin.Core.Model;

namespace Tacmin.Core.Infrastructure
{
    public class WebViewPageBase<T> : WebViewPage<T>
    {
        public YetkilerModel yetkiTanimlari = new YetkilerModel();
        public string Title
        {
            get => ViewBag.Title ?? "";
            set => ViewBag.Title = value;
        }

        protected override void InitializePage()
        {
            base.InitializePage();
        }

        public override void InitHelpers()
        {
            base.InitHelpers();
        }

        public override void Execute()
        {

        }
    }
}
