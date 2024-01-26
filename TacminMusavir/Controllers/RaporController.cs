using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tacmin.Core;
using Tacmin.Core.Interface;
using Tacmin.Service;

namespace TacminMusavir.Controllers
{
    public class RaporController : BaseController
    {
        private readonly IVirtualContext vctx;
        private readonly RaporService service;

        public RaporController(IVirtualContext _vctx, RaporService _service)
        {
            vctx = _vctx;
            service = _service;
        }
        // GET: Rapor
        public ActionResult GonderimRaporu([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {
                var liste = service.Whats_App_GonderimRaporu();

                var response = liste.ToDataSourceResult(req);

                return Json(response);
            }
            return View();
        }
        public ActionResult GunlukIslemRaporu([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {
                var liste = service.GunlukIslemListesi();

                var response = liste.ToDataSourceResult(req);

                return Json(response);
            }

            return View();
        }

        [HttpPost]
        public ActionResult Whats_App_GonderimRaporuTarih(DateTime? ilkTarih, DateTime? sonTarih)
        {

            var result = service.Whats_App_GonderimRaporuTarih(ilkTarih, sonTarih);

            return Json(result);
        }

        public ActionResult MailGonderimRaporu([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {
                var liste = service.Mail_GonderimRaporu();

                var response = liste.ToDataSourceResult(req);

                return Json(response);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Mail_GonderimRaporuTarih(DateTime? ilkTarih, DateTime? sonTarih)
        {

            var result = service.Mail_GonderimRaporuTarih(ilkTarih, sonTarih);

            return Json(result);
        }

        public ActionResult SmsGonderimRaporu([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {
                var liste = service.Sms_GonderimRaporu();

                var response = liste.ToDataSourceResult(req);

                return Json(response);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Sms_GonderimRaporuTarih(DateTime? ilkTarih, DateTime? sonTarih)
        {

            var result = service.Sms_GonderimRaporuTarih(ilkTarih, sonTarih);

            return Json(result);
        }

        [HttpGet]
        public ActionResult GibBilgisiEksikFirmalar()
        {

            var result = service.GibBilgisiEksikFirmalar();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SgkBilgisiEksikFirmalar()
        {

            var result = service.SgkBilgisiEksikFirmalar();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult IslemHataListesi()
        {
            var result = service.IslemHataListesi();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}