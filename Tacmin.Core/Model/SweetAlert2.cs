using System.ComponentModel;
using Tacmin.Core.Extensions;

namespace Tacmin.Core.Model
{
    public enum SweetAlertPosition
    {
        [Description("top-start")]
        TopStart,

        [Description("top-end")]
        TopEnd,

        [Description("bottom-start")]
        BottomStart,

        [Description("bottom-end")]
        BottomEnd
    }
    public enum SweetAlertType
    {
        [Description("success")]
        Success,

        [Description("info")]
        Info,

        [Description("error")]
        Error,

        [Description("warning")]
        Warning
    }

    public class SweetAlert2
    {
        public string Title { get; set; } = "";
        public string Text { get; set; } = "";
        public int Timer { get; set; } = 0;
        public bool ShowConfirmButton { get; set; } = true;
        public bool Animation { get; set; } = true;
        public string ConfirmButtonClass { get; set; } = "btn btn-primary";
        public bool ButtonsStyling { get; set; } = false;
        public SweetAlertPosition Position { get; set; } = SweetAlertPosition.BottomEnd;
        public SweetAlertType Type { get; set; } = SweetAlertType.Info;

        public string SweetAlert()
        {
            var sweetAlert = $@"Swal.fire({{
                  position: '{Position.GetDescription()}',
                  type: '{Type.GetDescription()}',
                  title: '{Title}',
                  text: '{Text}',
                  showConfirmButton: {ShowConfirmButton.ToString().ToLower()},
                  animation: {Animation.ToString().ToLower()},
                  timer: {Timer},
                  confirmButtonClass: '{ConfirmButtonClass}',
                  buttonsStyling: {ButtonsStyling.ToString().ToLower()},
                }})";
            return sweetAlert;
        }
    }
}
