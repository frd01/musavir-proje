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
using Tacmin.Model.Kasa;
using Tacmin.Service;

namespace TacminMusavir.Controllers
{
    public class KasaController : BaseController
    {
        private readonly IVirtualContext vctx;
        private readonly KasaService service;

        public KasaController(IVirtualContext _vctx, KasaService _service)
        {
            vctx = _vctx;
            service = _service;
        }

        // GET: Kasa
        public ActionResult KasaKartListesi([DataSourceRequest] DataSourceRequest req)
        {

            if (Request.IsAjaxRequest())
            {
                var result = service.KasaKartListesi();

                var response = result.ToDataSourceResult(req);

                return Json(response);
            }

            return View();
        }

        public ActionResult KasaIslemListesi([DataSourceRequest] DataSourceRequest req)
        {

            if (Request.IsAjaxRequest())
            {
                var result = service.KasaIslemListesi();

                var response = result.ToDataSourceResult(req);

                return Json(response);
            }

            return View();
        }

        [HttpPost]
        public ActionResult KasaKartDetay(int? kasaId)
        {
            var model = new Tacmin.Model.Kasa.KartModel();

            if (kasaId != null)
            {
                model = service.GetKasaKart(kasaId);
            }

            return Json(model);
        }

        [HttpPost]
        public ActionResult YeniKasaKaydi(Tacmin.Model.Kasa.KartModel clientData)
        {
            var result = service.YeniKasaKaydi(clientData);

            return Json(result);
        }

        [HttpPost]
        public ActionResult KasaNakitIslem(string islemTur, KasaIslemModel clientData)
        {

            var result = service.KasaNakitIslem(islemTur, clientData);

            return Json(result);
        }

        [HttpPost]
        public ActionResult KasaAyrintiListe(int? kasaId)
        {

            var result = service.KasaAyrintiListe(kasaId);

            return Json(result);
        }

        [HttpPost]
        public ActionResult GetKasaIslem(int? islemId, string islemTur)
        {

            var result = service.GetKasaIslem(islemId, islemTur);

            return Json(result);
        }
        [HttpPost]
        public ActionResult KasaIslemGuncelleme(KasaIslemModel clientData, string islemTur)
        {
            var result = service.KasaIslemGuncelleme(clientData, islemTur);

            return Json(result);
        }
        [HttpPost]
        public ActionResult KasaIslemSil(int? islemId, string islemTur)
        {

            var result = service.KasaIslemSil(islemId, islemTur);

            return Json(result);
        }

        [HttpPost]
        public ActionResult KasaBakiyeRaporu(RaporRequestModel clientData)
        {

            var result = service.KasaBakiyeRaporu(clientData);

            return Json(result);
        }
        [HttpPost]
        public ActionResult KasaEsktraListesi(RaporRequestModel clientData)
        {

            var result = service.KasaEsktraListesi(clientData);

            return Json(result);
        }

        public JsonResult KasaIslemTakipListesi()
        {
            var result = service.KasaIslemTakipListesi();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult KasaParaBirimListesi()
        {

            var result = service.KasaParaBirimListesi();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CariFilterList()
        {

            var result = service.CariFilterList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult KasaKartFilterList()
        {

            var result = service.KasaKartFilterList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}