using Kendo.Mvc;
using Kendo.Mvc.Infrastructure;
using Kendo.Mvc.UI;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using Tacmin.Core.Extensions;

namespace Tacmin.Core.ViewExtensions
{
    public class TyToolBar : ToolBar
    {
        private readonly ViewContext _viewContext;

        public TyToolBar(
            ViewContext viewContext,
            IJavaScriptInitializer initializer,
            IUrlGenerator urlGenerator
        ) : base(viewContext, initializer, urlGenerator)
        {
            if (Name.IsEmpty())
                Name = "tacminToolbar";

            _viewContext = viewContext;
        }

        protected override void WriteHtml(HtmlTextWriter writer)
        {
            base.WriteHtml(writer);

            foreach (TyToolBarItem item in Items.Where(x => x.GetType() == typeof(TyToolBarItem) && (x as TyToolBarItem).ModalButton))
            {
                if (item.ButtonType == TyToolBarButtonType.Filter)
                {
                    writer.WriteLine($"<div style='display:none;' id='{item.ModalName}'>{item.ModalContent}</div>");
                }
                else if (item.ButtonType == TyToolBarButtonType.Request)
                {
                    writer.WriteLine($"<div style='display:none;' id='{item.ModalName}'>");
                    writer.WriteLine($"<form id='form_{item.ModalName}' action='{item.PostUrl}'>{item.ModalContent}</form>");
                    writer.WriteLine("</div>");
                }
            }
        }

        public override void WriteInitializationScript(TextWriter writer)
        {
            base.WriteInitializationScript(writer);

            foreach (TyToolBarItem item in Items)
            {
                string js = null;
                if (item.ModalButton)
                {
                    js = $@"
                        function {item.Click.HandlerName}() {{
                            $('#{item.ModalName}').kendoWindow({{width: 600, height: 500, title: 'Filtrele'}}).data('kendoWindow').open().center();
                        }}
                    ";
                }
                else if (item.Id == "tbDeleteBtn")
                {
                    js = $@"
                        $(document).on('click', '#tbDeleteBtn', function(){{
                            kendo.confirm('Kaydı silmek istediğinize emin misiniz?').then(function(){{
                                {item.Click.HandlerName}();
                            }});
                        }})
                    ";
                }
                else if (item.ButtonType == TyToolBarButtonType.Downnload)
                {
                    js = $@"
                        function {item.Click.HandlerName}(){{
                            var grid = $('#{item.GridName}').data('kendoGrid');

                            var parameterMap = grid.dataSource.transport.parameterMap;
                            var requestObject = parameterMap({{
                                filter: grid.dataSource.filter()
                            }});

                            var req = $.ajax({{
                                url: '{item.PostUrl}',
                                data: requestObject,
                                xhrFields: {{
                                    responseType: 'blob'
                                }}
                            }});

                            req.done(function(res) {{
                                var blob = new Blob([res], {{ type: 'application/zip' }});
                                var url = URL.createObjectURL(blob);
                                var a = document.createElement('a');
                                a.href = url;
                                document.body.appendChild(a);
                                a.click();
                            }});
                        }}
                    ";
                }
                else if (item.ButtonType == TyToolBarButtonType.Print)
                {
                    js = $@"
                        function {item.Click.HandlerName}(e) {{
                            if ($.ty.utils.check(e.id)) {{
                                var grid = $('#{item.GridName}').data('kendoGrid');

                                var parameterMap = grid.dataSource.transport.parameterMap;
                                var requestObject = parameterMap({{
                                    filter: grid.dataSource.filter(),
                                    pdftype: e.id
                                }});

                                var req = $.ajax({{
                                    url: '{item.PostUrl}',
                                    data: requestObject,
                                    xhrFields: {{
                                        responseType: 'blob'
                                    }}
                                }});

                                req.done(function (res) {{
                                    var blob = new Blob([res], {{ type: 'application/pdf' }});
                                    var url = URL.createObjectURL(blob);

                                    window.open(url, '_blank');
                                }});
                            }}
                        }}
                    ";
                }

                if (js != null)
                    writer.WriteLine(js);
            }
        }
    }
}
