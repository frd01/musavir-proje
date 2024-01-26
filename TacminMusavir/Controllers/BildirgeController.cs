using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using Tacmin.Core;
using Tacmin.Core.Interface;
using Tacmin.Model;
using Tacmin.Model.Bildirge;
using Tacmin.Model.Ortak;
using Tacmin.Model.Whatsapp;
using Tacmin.Service;

namespace TacminMusavir.Controllers
{
    public class BildirgeController : BaseController
    {
        private readonly BildirgeService _bildirgeService;
        private readonly IVirtualContext vctx;

        public BildirgeController(
            BildirgeService bildirgeService,
            IVirtualContext vctx
            )
        {
            _bildirgeService = bildirgeService;
            this.vctx = vctx;
        }

        [Yetki(nameof(Yetkiler.BILDIRGE_LIST_GORUNTULE))]
        public ActionResult BildirgeListesi([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {
                //var res = _bildirgeService.GetBildirge<BildirgeAraModel>(req);
                var liste = new DataIslemler.Listeler.Bildirge.Listeler(vctx.DataAdi).GetBildirgeListesi();

                var res = liste.ToDataSourceResult(req);
                return Json(res);
            }

            return View();
        }

        public ActionResult SorguBildirgeListesi([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {
                //var res = _bildirgeService.GetBildirge<BildirgeAraModel>(req);
                var _tarih = this.Request.QueryString["tarih"];
                var tarih = Convert.ToDateTime(_tarih);
                var liste = new DataIslemler.Listeler.Bildirge.Listeler(vctx.DataAdi).GetBildirgeListesi(tarih);

                var res = liste.ToDataSourceResult(req);
                return Json(res);
            }

            return View();
        }

        public ActionResult ViziteHastalik([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {

                var _tarih = this.Request.QueryString["tarih"];
                var tarih = Convert.ToDateTime(_tarih);
                var liste = _bildirgeService.SorguViziteListe(tarih, "3");

                var response = liste.ToDataSourceResult(req);

                return Json(response);
            }

            return View();
        }

        public ActionResult ViziteMeslekHast([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {

                var _tarih = this.Request.QueryString["tarih"];
                var tarih = Convert.ToDateTime(_tarih);
                var liste = _bildirgeService.SorguViziteListe(tarih, "2");

                var response = liste.ToDataSourceResult(req);

                return Json(response);
            }

            return View();
        }

        public ActionResult ViziteAnalik([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {

                var _tarih = this.Request.QueryString["tarih"];
                var tarih = Convert.ToDateTime(_tarih);
                var liste = _bildirgeService.SorguViziteListe(tarih, "4");

                var response = liste.ToDataSourceResult(req);

                return Json(response);
            }

            return View();
        }
        public ActionResult ViziteIsKazasi([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {

                var _tarih = this.Request.QueryString["tarih"];
                var tarih = Convert.ToDateTime(_tarih);
                var liste = _bildirgeService.SorguViziteListe(tarih, "1");

                var response = liste.ToDataSourceResult(req);

                return Json(response);
            }

            return View();
        }

        public ActionResult ViziteOnaysizListe([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {
                var sonTarih = DateTime.Now;
                var ilkTarih = sonTarih.AddMonths(-1);

                var liste = _bildirgeService.ViziteListesi(false, ilkTarih, sonTarih);

                var response = liste.ToDataSourceResult(req);

                return Json(response);
            }

            return View();
        }

        public ActionResult ViziteOnayliListe([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {
                var sonTarih = DateTime.Now;
                var ilkTarih = sonTarih.AddMonths(-1);

                var liste = _bildirgeService.ViziteListesi(true, ilkTarih, sonTarih);

                var response = liste.ToDataSourceResult(req);

                return Json(response);
            }

            return View();
        }

        public ActionResult IseGirisCikislar()
        {
            return View();
        }

        public ActionResult SorguCikislar([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {
                var _tarih = this.Request.QueryString["tarih"];
                var tarih = Convert.ToDateTime(_tarih);

                var liste = _bildirgeService.SorguListe(tarih, "çıkış");

                var response = liste.ToDataSourceResult(req);

                return Json(response);
            }

            return View();
        }

        public ActionResult SorguGirisler([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {
                var _tarih = this.Request.QueryString["tarih"];
                var tarih = Convert.ToDateTime(_tarih);

                var liste = _bildirgeService.SorguListe(tarih, "giriş");

                var response = liste.ToDataSourceResult(req);

                return Json(response);
            }

            return View();
        }

        [Yetki(nameof(Yetkiler.BILDIRGE_HIZLIST_GORUNTULE))]
        public ActionResult HizmetListesiPdfGor(int? id)
        {
            if (id != null)
            {
                var file = _bildirgeService.HizmetListesiPdfGor(id);
                if (file.file.Length > 0)
                {
                    return FileStreamResult(file.file, "application/pdf", file.fileName);
                }
            }

            return Content("Pdf bulunamadı");
        }

        [Yetki(nameof(Yetkiler.BILDIRGE_TAHAKKUK_GORUNTULE))]
        public ActionResult TahakkukPdfGor(int? id)
        {
            if (id != null)
            {
                var file = _bildirgeService.TahakkukPdfGor(id);
                if (file.file.Length > 0)
                {
                    return FileStreamResult(file.file, "application/pdf", file.fileName);
                }
            }

            return Content("Pdf bulunamadı");
        }

        public ActionResult BildirgeZipDownload(List<BildirgeAraModel> bildirge_listesi, BildirgeBelgeModel belgeModel)
        {
            var file = _bildirgeService.BildirgeZipOlustur(bildirge_listesi,belgeModel);

            return File(file.file, "application/zip", file.fileName);
        }

        public ActionResult BildirgeTopluPdf([DataSourceRequest] DataSourceRequest req, string pdftype)
        {
            var file = _bildirgeService.BildirgeTopluPdfOlustur(req, pdftype);
            return File(file.file, "application/pdf", file.fileName);
        }

        [HttpPost]
        public ActionResult BildirgeSilme(SecilenBildirgeler bildirge_listesi)
        {
            var sw = new Stopwatch();
            sw.Start();
            //new DataIslemler.Islemler.Beyanname.BeyannameIslem().BeyannameSil(beyanname_listesi.Beyannameler);
            sw.Stop();

            return Redirect(Url.RouteUrl(new { controller = "beyanname", action = "BeyannameListesi" }));
        }

        [HttpPost]
        public ActionResult GonderimIcerikOlustur(List<BildirgeAraModel> clientList, string gonderimTur)
        {

            var result = _bildirgeService.GonderimIcerikOlustur(clientList, gonderimTur);

            return Json(result);
        }

        [HttpPost]
        public ActionResult GonderimDataKayit(List<FirmaModel> clientList, List<BildirgeAraModel> bildirgeListesi)
        {

           var result =  _bildirgeService.GonderimDataKayit(clientList, bildirgeListesi);

            return Json(result);
        }
        [HttpPost]
        public ActionResult ViziteKayitIslem(List<ViziteResponseModel> clientList)
        {
            var result = _bildirgeService.ViziteKayitIslem(clientList);

            return Json(result);
        }
        [HttpPost]
        public ActionResult ViziteListesi(bool? durum, DateTime? ilkTarih, DateTime? sonTarih)
        {
            var result = _bildirgeService.ViziteListesi(durum,ilkTarih,sonTarih);

            return Json(result);
        }
        [HttpGet]
        public ActionResult FirmaSubeListesi()
        {
            var result = _bildirgeService.FirmaSubeListesi();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult IseGirisTarihAralikListesi(DateTime? ilkTarih, DateTime? sonTarih)
        {
            var result = _bildirgeService.IseGirisTarihAralikListesi(ilkTarih, sonTarih);

            return Json(result);

        }
        [HttpPost]
        public ActionResult IseGirisCikisKaydet(List<IseGirisResModel> clientList)
        {
            var result = _bildirgeService.IseGirisCikisKaydet(clientList);

            return Json(result);
        }
        [HttpPost]
        public ActionResult IseGirisCikisListesi(DateTime? ilkTarih, DateTime? sonTarih)
        {
            var result = _bildirgeService.IseGirisCikisListesi(ilkTarih, sonTarih);

            return Json(result);
        }

        public ActionResult GetPdfEvrak(int id)
        {
            var pdf_file = _bildirgeService.GetPdfEvrak(id);

            if (pdf_file!=null)
            {
                return FileStreamResult(pdf_file.Icerik, "application/pdf", pdf_file.DosyaAdi);
            }

            return Content("Pdf bulunamadı");
        }
    }

    public class SecilenBildirgeler
    {
        public List<int> Bildirgeler { get; set; }
    }

    public class BildirgeData
    {

        public int id { get; set; }
    }
}