using Kendo.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.UI.Fluent;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Tacmin.Core.ViewExtensions
{
    public class TyToolBarItemBuilder : ToolBarItemBuilder
    {
        private readonly TyToolBarItem _settings;

        public TyToolBarItemBuilder(TyToolBarItem settings) : base(settings)
        {
            _settings = settings;
        }

        public TyToolBarItemBuilder Filter(Func<object, HelperResult> content, string url = null, string modalName = "FilterModal", string clickEvent = "onClickFilterModal")
        {
            _settings.Type = CommandType.Button;
            _settings.Text = "Filtrele";
            _settings.Icon = "filter";
            _settings.ModalButton = true;
            _settings.ButtonType = TyToolBarButtonType.Filter;
            _settings.ModalUrl = url;
            _settings.ModalName = modalName;
            _settings.DialogName = "Filtrele";
            _settings.ModalContent = content.Invoke(null).ToHtmlString();
            _settings.Click = new ClientHandlerDescriptor { HandlerName = clickEvent };

            return this;
        }

        public TyToolBarItemBuilder Print(string url = null, string gridName = "master", string clickEvent = "onClickPrintPdfFiles")
        {
            _settings.Type = CommandType.SplitButton;
            _settings.Text = "Yazdır";
            _settings.Icon = "print";
            _settings.Click = new ClientHandlerDescriptor { HandlerName = clickEvent };
            _settings.PostUrl = url;
            _settings.GridName = gridName;
            _settings.ButtonType = TyToolBarButtonType.Print;

            return this;
        }

        public TyToolBarItemBuilder Share(string clickEvent = "share_click_event")
        {
            _settings.Type = CommandType.SplitButton;
            _settings.Text = "Gönder";
            _settings.Icon = "share";
            _settings.Click = new ClientHandlerDescriptor { HandlerName = clickEvent };
            _settings.MenuButtons = new List<ToolBarItemMenuButton>
            {
                new ToolBarItemMenuButton{ Text = "Whatsapp",Id="whats_app" },
                 new ToolBarItemMenuButton{ Text = "Mail",Id="mail" },
                new ToolBarItemMenuButton{ Text = "SMS",Id="sms" }
            };

            return this;
        }

        public TyToolBarItemBuilder BilgiGuncelle(string clickEvent = "bilgi_guncelleme_click_event")
        {
            _settings.Type = CommandType.SplitButton;
            _settings.Text = "Bilgi Güncelleme";
            _settings.Icon = "clipboard-markdown";
            _settings.Click = new ClientHandlerDescriptor { HandlerName = clickEvent };
            _settings.MenuButtons = new List<ToolBarItemMenuButton>
            {
                new ToolBarItemMenuButton{ Text = "Gib Bilgileri",Id="gib_bil" },
                 new ToolBarItemMenuButton{ Text = "İletişim Bilgileri",Id="ilt_bil" },
                new ToolBarItemMenuButton{ Text = "Sgk Şifreleri",Id="sgk_bil" }
            };

            return this;
        }

        public TyToolBarItemBuilder Download(string url = null, string gridName = "master", string clickEvent = "onClickDownloadZipFile")
        {
            _settings.Type = CommandType.Button;
            _settings.Text = "İndir";
            _settings.Icon = "download";
            _settings.Click = new ClientHandlerDescriptor { HandlerName = clickEvent };
            _settings.PostUrl = url;
            _settings.GridName = gridName;
            _settings.ButtonType = TyToolBarButtonType.Downnload;

            return this;
        }

        public TyToolBarItemBuilder Download_Split(string url = null, string gridName = "master", string clickEvent = "onClickDownloadZipFile",string label="İndir",string id = "spt_dowload")
        {
            _settings.Type = CommandType.SplitButton;
            _settings.Text = label;
            _settings.Icon = "download";
            _settings.Click = new ClientHandlerDescriptor { HandlerName = clickEvent };
            _settings.PostUrl = url;
            _settings.GridName = gridName;
            _settings.ButtonType = TyToolBarButtonType.Downnload;
            _settings.Id = id;

            return this;
        }

        public TyToolBarItemBuilder Request(MvcHtmlString content, string url = null, string modalName = "RequestModal", string clickEvent = "onClickRequestModal", string dialogname = "Filtrele", string label = "Sorgula")
        {
            _settings.Type = CommandType.Button;             
            _settings.Text = label;
            _settings.Icon = "search";
            _settings.ModalButton = true;
            _settings.ButtonType = TyToolBarButtonType.Request;
            _settings.ModalUrl = url;
            _settings.ModalName = modalName;
            _settings.DialogName = dialogname;
            _settings.ModalContent = content.ToHtmlString();
            _settings.Id = "sorgulama";
            _settings.Click = new ClientHandlerDescriptor { HandlerName = clickEvent };

            return this;
        }

        public TyToolBarItemBuilder Request_Custom(MvcHtmlString content, string url = null, string modalName = "RequestModal", string clickEvent = "onClickRequestModal", string dialogname = "Filtrele", string label = "Sorgula")
        {
            _settings.Type = CommandType.Button;
            _settings.Text = label;
            _settings.Icon = "search";
            _settings.ModalButton = true;
            _settings.ButtonType = TyToolBarButtonType.Request;
            _settings.ModalUrl = url;
            _settings.ModalName = modalName;
            _settings.DialogName = dialogname;
            _settings.ModalContent = content.ToHtmlString();
            _settings.Id = "dataArama";
            _settings.Click = new ClientHandlerDescriptor { HandlerName = clickEvent };

            return this;
        }

        public TyToolBarItemBuilder PostUrl(string url)
        {
            _settings.PostUrl = url;
            return this;
        }

        public TyToolBarItemBuilder New()
        {
            _settings.Type = CommandType.Button;
            _settings.Text = "Yeni Ekle";
            _settings.Icon = "plus";

            return this;
        }

        public TyToolBarItemBuilder Delete(string id = "tbDeleteBtn", string clickEvent = "tbDeleteBtnOnClick")
        {
            _settings.Type = CommandType.Button;
            _settings.Text = "Sil";
            _settings.Icon = "close";
            _settings.Id = id;
            _settings.Click = new ClientHandlerDescriptor { HandlerName = clickEvent };

            return this;
        }

        public TyToolBarItemBuilder SilBtn(string id = "btn_sil", string clickEvent = "btn_sil_event")
        {
            _settings.Type = CommandType.Button;
            _settings.Text = "Sil";
            _settings.Icon = "close";
            _settings.Id = id;
            _settings.Click = new ClientHandlerDescriptor { HandlerName = clickEvent };

            return this;
        }

        public TyToolBarItemBuilder MknBilgiGnc(string id = "btn_mal_gnc", string clickEvent = "btn_mal_gnc_event")
        {
            _settings.Type = CommandType.Button;
            _settings.Text = "Bilgi Güncelleme";
            _settings.Icon = "save";
            _settings.Id = id;
            _settings.Click = new ClientHandlerDescriptor { HandlerName = clickEvent };

            return this;
        }

        public TyToolBarItemBuilder Save()
        {
            _settings.Type = CommandType.Button;
            _settings.Text = "Kaydet";
            _settings.Icon = "save";

            return this;
        }

        public TyToolBarItemBuilder Btn_Custom(string id = "btn_mal_gnc", string clickEvent = "btn_mal_gnc_event", string label = "button",string icon="save")
        {
            _settings.Type = CommandType.Button;
            _settings.Text = label;
            _settings.Icon = icon;
            _settings.Id = id;
            _settings.Click = new ClientHandlerDescriptor { HandlerName = clickEvent };



            return this;
        }


    }
}
