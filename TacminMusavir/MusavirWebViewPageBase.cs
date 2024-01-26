using System.Collections.Generic;
using Tacmin.Core;
using Tacmin.Core.Infrastructure;
using Tacmin.Core.Model;

namespace TacminMusavir
{
    public class MusavirWebViewPageBase<T> : WebViewPageBase<T>
    {
        public List<SistemMenuModel> mainMenu = new TacminMenuProvider().BuildMenu();
        public TacminVirtualContext VirtualContext = Engine.Instance.Resolve<TacminVirtualContext>();
    }
}