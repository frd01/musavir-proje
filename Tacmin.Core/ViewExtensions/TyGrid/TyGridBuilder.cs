using Kendo.Mvc.UI.Fluent;
using System;

namespace Tacmin.Core.ViewExtensions
{
    public class TyGridBuilder<T> : GridBuilder<T> where T : class
    {
        public TyGrid<T> TyGridComponent => ((TyGrid<T>)Component);

        public TyGridBuilder(TyGrid<T> component) : base(component)
        {
            this.Pageable(x => x.Enabled(true).Refresh(true))
                .Sortable()
                //.ColumnMenu()
                .Resizable(r => r.Columns(true))
                .Reorderable(r => r.Columns(true))
                .Height(TyGridComponent.ViewContext.RequestContext.HttpContext.Request.Browser.ScreenPixelsHeight * 2 - 150)
                .Scrollable()
                .Navigatable()
                .Pageable(p => p.PageSizes(new int[] { 20, 50, 100 }).Messages(msg => msg.Display("{2} öğeden {0}-{1} arası gösteriliyor").ItemsPerPage("Sayfa başına öğe")))
                .Filterable()
                .Excel(x => x.AllPages(true).FileName(DateTime.Now.ToString("ddMMyyyyHHmm") + "-Export.xlsx"));
        }

        public TyGridBuilder<T> Columns(Action<TyGridColumnFactory<T>> configurator)
        {
            var obj = new TyGridColumnFactory<T>(TyGridComponent, TyGridComponent.ViewContext, TyGridComponent.UrlGenerator);
            configurator(obj);
            return this;
        }

        public new TyGridBuilder<T> Name(string componentName)
        {
            TyGridComponent.Name = componentName;
            TyGridComponent.DataSourceId = $"{componentName}ds";
            return this;
        }

        public TyGridBuilder<T> Filterable(bool state)
        {
            Component.Filterable.Enabled = state;
            return this;
        }

        public new GridBuilder<T> DataSource(Action<DataSourceBuilder<T>> configurator)
        {
            TyGridComponent.DataSourceId = null;
            return base.DataSource(configurator);
        }
    }
}
