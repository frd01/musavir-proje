using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tacmin.Core;
using Tacmin.Core.Interface;
using Tacmin.Model;
using Tacmin.Model.Banka;
using Tacmin.Service;

namespace TacminMusavir.Controllers
{
    public class BankaController : BaseController
    {

        private readonly IVirtualContext vctx;
        private readonly BankaService service;

        public BankaController(IVirtualContext _vctx, BankaService _service)
        {
            vctx = _vctx;
            service = _service;
        }
        // GET: Banka
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BankaKartListesi([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {
                var liste = service.BankaKartListesi();

                var response = liste.ToDataSourceResult(req);

                return Json(response);
            }

            return View();
        }

        public ActionResult BankaIslemListesi()
        {

            return View();
        }

        /*public ActionResult BankaIslemListesi([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {
                var liste = service.BankaIslemListesi();

                var response = liste.ToDataSourceResult(req);

                return Json(response);
            }

            return View();
        }*/

        [HttpPost]
        public ActionResult GetBankaIslemListesi(int? bankaId)
        {

            var result = service.BankaIslemListesi(bankaId);

            return Json(result);
        }

        [HttpPost]
        public ActionResult YeniBankaKartKaydi(BankaKartModel clientData)
        {

            var result = service.YeniBankaKartKaydi(clientData);

            return Json(result);
        }

        [HttpPost]
        public ActionResult BankaIslemKayit(BankaIslemModel clientData, string islemTur)
        {

            var result = service.BankaIslemKayit(clientData, islemTur);

            return Json(result);
        }

        [HttpPost]
        public ActionResult BankaKayitSil(BankaIslemModel clientData, string islemTur)
        {

            var result = service.BankaKayitSil(clientData, islemTur);

            return Json(result);
        }
        [HttpPost]
        public ActionResult BankaKartGuncelleme(BankaKartModel clientData)
        {

            var result = service.BankaKartGuncelleme(clientData);
            return Json(result);
        }
        [HttpPost]
        public ActionResult GetBankaIslem(int? islemId, string islemTur)
        {
            var result = service.GetBankaIslem(islemId, islemTur);

            return Json(result);
        }

        [HttpPost]
        public ActionResult BankaIslemGuncelleme(BankaIslemModel clientData, string islemTur)
        {

            var result = service.BankaIslemGuncelleme(clientData, islemTur);

            return Json(result);
        }
        [HttpPost]
        public ActionResult BankaBakiyeRaporu(Tacmin.Model.RaporRequestModel clientData)
        {
            var result = service.BankaBakiyeRaporu(clientData);

            return Json(result);
        }

        [HttpPost]
        public ActionResult GetBankaEkstraRapor(RaporRequestModel clientData)
        {

            var result = service.GetBankaEkstraRapor(clientData);

            return Json(result);
        }

        public JsonResult GetHesapTurListesi()
        {
            var result = service.HesapTurListesi();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetParaBirimListesi()
        {
            var result = service.ParaBirimListesi();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBankaKartListesi()
        {

            var result = service.GetBankaKartListesi();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCariListesi()
        {

            var result = service.GetCariListesi();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetKasaListesi()
        {

            var result = service.GetKasaListesi();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}