using Kendo.Mvc.UI;
using System.ComponentModel;

namespace Tacmin.Core.ViewExtensions
{
    public enum TyToolBarButtonType
    {
        Filter,
        Request,
        Downnload,
        Print
    }

    public class TyToolBarItem : ToolBarItem
    {
        public bool ModalButton { get; set; }

        public string ModalName { get; set; }

        public string ModalUrl { get; set; }

        public string ModalContent { get; set; }

        public TyToolBarButtonType ButtonType { get; set; }

        public string PostUrl { get; set; }

        public string GridName { get; set; }
        [DefaultValue("")]
        public string DialogName { get; set; }
    }
}
