using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tacmin.Core;
using Tacmin.Core.Interface;
using Tacmin.Model.Maliye;
using Tacmin.Service;

namespace TacminMusavir.Controllers
{
    public class GelenFaturaController : BaseController
    {

        private readonly IVirtualContext vctx;
        private readonly GelenFaturaService service;

        public GelenFaturaController(IVirtualContext _vctx, GelenFaturaService _service)
        {
            vctx = _vctx;
            service = _service;
        }
        // GET: GelenFatura
        public ActionResult GelenFaturaListesi()
        {
            

            return View();
        }

        public ActionResult SorguGelenFaturaListesi([DataSourceRequest] DataSourceRequest req)
        {

            if (Request.IsAjaxRequest())
            {
                var _tarih = this.Request.QueryString["tarih"];
                var tarih = Convert.ToDateTime(_tarih);

                var liste = service.SorguGelenFaturaListesi(tarih);

                var response = liste.ToDataSourceResult(req);

                return Json(response);
            }

            return View();
        }

        [HttpPost]
        public ActionResult SorguGelenFaturaListesi(DateTime? ilkTarih, DateTime? sonTarih,Tacmin.Model.LocalData.FirmaModel firma)
        {
            var result = service.GelenFaturaListesi(ilkTarih, sonTarih,firma);

            return Json(result);
        }

        [HttpPost]
        public ActionResult TarihAralikListesi(DateTime? ilkTarih, DateTime? sonTarih)
        {
            if (ilkTarih == null)
            {
                sonTarih = DateTime.Now;
                ilkTarih = new DateTime(sonTarih.Value.Year, sonTarih.Value.Month, 1);
            }

            var result = service.TarihAralikListesi(ilkTarih, sonTarih);

            return Json(result);
        }

        [HttpPost]
        public ActionResult TarihAralikListesiSonBirAylik(DateTime? ilkTarih, DateTime? sonTarih)
        {
            if (ilkTarih == null)
            {
                sonTarih = DateTime.Now;
                ilkTarih = new DateTime(sonTarih.Value.Year, sonTarih.Value.Month, 1);
                ilkTarih = ilkTarih.Value.AddMonths(-1);
            }

            var result = service.TarihAralikListesi(ilkTarih, sonTarih);

            return Json(result);
        }

        [HttpPost]
        public ActionResult FaturaKayit(List<GelenFaturaModel> clientList,int? firmaId)
        {
            var result = service.FaturaKayit(clientList,firmaId);

            return Json(result);
        }

        [HttpPost]
        public ActionResult FaturOtoKayit(List<GelenFaturaModel> clientList)
        {
            var result = service.FaturOtoKayit(clientList);

            return Json(result);
        }

        [HttpPost]
        public ActionResult GelenFaturaSorguIslem(DateTime? ilkTarih, DateTime? sonTarih, Tacmin.Model.LocalData.FirmaModel firma)
        {
            var result = service.GelenFaturaSorguIslem(ilkTarih, sonTarih, firma);

            return Json(result);
        }

        public JsonResult GetFirmaListesi()
        {

            var result = service.GetFirmaListesi();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}