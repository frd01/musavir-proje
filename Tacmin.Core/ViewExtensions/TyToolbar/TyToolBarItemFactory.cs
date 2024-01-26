using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using System.Collections.Generic;

namespace Tacmin.Core.ViewExtensions
{
    public class TyToolBarItemFactory : ToolBarItemFactory
    {
        private List<ToolBarItem> _container;

        public TyToolBarItemFactory(List<ToolBarItem> container) : base(container)
        {
            _container = container;
        }

        public new TyToolBarItemBuilder Add()
        {
            var toolBarItem = new TyToolBarItem();
            _container.Add(toolBarItem);
            return new TyToolBarItemBuilder(toolBarItem);
        }
    }
}
