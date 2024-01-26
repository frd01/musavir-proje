using System.Collections.Generic;
using Tacmin.Core.Model;
using Tacmin.Core.Utilities;

namespace TacminMusavir
{
    public class TacminMenuProvider
    {
        public SistemMenuModel SistemMenuModel()
        {
            var model = new SistemMenuModel();
            return model;
        }

        private static readonly YetkilerModel yetkiler = new YetkilerModel();

        public List<SistemMenuModel> BuildMenu()
        {
            var model = new List<SistemMenuModel>();

            model.Add(SistemMenuModel().Url(Url.RouteUrl(new { controller = "main", action = "index" })).Title("Anasayfa").Icon("fas fa-home").Yetki(yetkiler.DASHBORD));

            model.Add(SistemMenuModel().Title("Mükellef İşlemleri").Icon("fas fa-users").SubMenu(new List<SistemMenuModel> {
                SistemMenuModel().Url(Url.RouteUrl(new { controller = "mukellef", action = "mukellefkayit" })).Title("Mükellef Kayıt").Yetki(yetkiler.MUKELLEF_KAYIT_GOR),
                SistemMenuModel().Url(Url.RouteUrl(new { controller = "mukellef", action = "mukelleflistesi" })).Title("Mükellef Listesi").Yetki(yetkiler.MUKELLEF_LIST_GORUNTULE),
                SistemMenuModel().Url(Url.RouteUrl(new { controller = "vergiLevha", action = "vergiLevhaListe" })).Title("Vergi Levha Listesi").Yetki(yetkiler.MUKELLEF_LIST_GORUNTULE),
                 SistemMenuModel().Url(Url.RouteUrl(new { controller = "mukellef", action = "BeyannameTakipListesi" })).Title("Beyanname Takip Listesi").Yetki(yetkiler.MUKELLEF_LIST_GORUNTULE),
                 SistemMenuModel().Url(Url.RouteUrl(new { controller = "mukellef", action = "FirmaBeyannameDurumListesi" })).Title("Firma Beyanname Durum Listesi").Yetki(yetkiler.MUKELLEF_LIST_GORUNTULE)
            }));

            model.Add(SistemMenuModel().Title("Maliye İşlemleri").Icon("fas fa-cash-register").SubMenu(new List<SistemMenuModel>
            {
                SistemMenuModel().Url(Url.RouteUrl(new { controller = "Beyanname", action = "BeyannameListesi" })).Title("Beyannameler").Yetki(yetkiler.BEYANNAME_LIST_GORUNTULE),
            }));
            model.Add(SistemMenuModel().Title("SGK İşlemleri").Icon("far fa-hospital").SubMenu(new List<SistemMenuModel>
            {
                SistemMenuModel().Url(Url.RouteUrl(new { controller = "Bildirge", action = "BildirgeListesi" })).Title("Bildirgeler").Yetki(yetkiler.BILDIRGE_LIST_GORUNTULE),
            }));

            model.Add(SistemMenuModel().Title("E-Arşiv").Icon("fas fa-cash-register").SubMenu(new List<SistemMenuModel>
            {
                SistemMenuModel().Url(Url.RouteUrl(new { controller = "GelenFatura", action = "FaturaListesi" })).Title("Gelen Faturalar").Yetki(yetkiler.BEYANNAME_LIST_GORUNTULE),
                SistemMenuModel().Url(Url.RouteUrl(new { controller = "GidenFatura", action = "FaturaListesi" })).Title("Giden Faturalar").Yetki(yetkiler.BEYANNAME_LIST_GORUNTULE),

            }));

            model.Add(SistemMenuModel().Title("Tebligatlar").Icon("fas fa-cash-register").SubMenu(new List<SistemMenuModel>
            {
                SistemMenuModel().Url(Url.RouteUrl(new { controller = "gelirtebligat", action = "TebligatListesi" })).Title("Gelir İdaresi Başkanlığı").Yetki(yetkiler.BEYANNAME_LIST_GORUNTULE),
                SistemMenuModel().Url(Url.RouteUrl(new { controller = "icisleritebligat", action = "TebligatListesi" })).Title("İçişleri Bakanlığı").Yetki(yetkiler.BEYANNAME_LIST_GORUNTULE),


            }));

            model.Add(SistemMenuModel().Title("Raporlar").Icon("fas fa-cash-register").SubMenu(new List<SistemMenuModel>
            {
                SistemMenuModel().Url(Url.RouteUrl(new { controller = "rapor", action = "GonderimRaporu" })).Title("Whatsapp Raporu").Yetki(yetkiler.BEYANNAME_LIST_GORUNTULE),
               


            }));

            /* model.Add(SistemMenuModel().Title("Tebligatlar").Icon("fas fa-cash-register").SubMenu(new List<SistemMenuModel>
             {
                 SistemMenuModel().Url(Url.RouteUrl(new { controller = "gelirTebligat", action = "gelirTebligatListesi" })).Title("Gelir İdaresi").Yetki(yetkiler.BEYANNAME_LIST_GORUNTULE)

             }));*/



            model.Add(SistemMenuModel().Title("Hesap Yönetimi").Icon("fas fa-key").SubMenu(new List<SistemMenuModel> {
                SistemMenuModel().Url(Url.RouteUrl(new { controller = "Hesap", action = "Hesabim" })).Title("Hesabım")
            }));

            model.Add(SistemMenuModel().Url(Url.RouteUrl(new { controller = "login", action = "CikisYap" })).Title("Çıkış").Icon("fa-solid fa-right-from-bracket").Yetki(yetkiler.DASHBORD));

            // <i class="fa-solid fa-right-from-bracket"></i>

            return model;
        }
    }
}