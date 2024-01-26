using System.Collections.Generic;

namespace Tacmin.Core.Model
{
    public class SistemMenuModel
    {
        public string URL { get; set; } = "#";

        public string TARGET { get; set; } = "_self";

        public string TITLE { get; set; }

        public string ICONCLASS { get; set; } = "fas fa-dot-circle";

        public SistemYetkiModel YETKI { get; set; }

        public List<SistemMenuModel> SUBMENU { get; set; }

        public SistemMenuModel Url(string url)
        {
            URL = url;
            return this;
        }

        public SistemMenuModel Target(string target)
        {
            TARGET = target;
            return this;
        }

        public SistemMenuModel Title(string title)
        {
            TITLE = title;
            return this;
        }

        public SistemMenuModel Icon(string iconclass)
        {
            ICONCLASS = iconclass;
            return this;
        }

        public SistemMenuModel Yetki(SistemYetkiModel yetki)
        {
            YETKI = yetki;
            return this;
        }

        public SistemMenuModel SubMenu(List<SistemMenuModel> submenu)
        {
            if (SUBMENU == null)
                SUBMENU = new List<SistemMenuModel>();

            SUBMENU.AddRange(submenu);
            return this;
        }
    }
}
