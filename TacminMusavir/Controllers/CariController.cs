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
    public class CariController : BaseController
    {
        private readonly IVirtualContext vctx;
        private readonly CariService service;

        public CariController(IVirtualContext _vctx, CariService _service)
        {
            vctx = _vctx;
            service = _service;
        }
        // GET: Cari
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CariListesi([DataSourceRequest] DataSourceRequest req)
        {

            if (Request.IsAjaxRequest())
            {
                var liste = service.CariListesi();

                var response = liste.ToDataSourceResult(req);

                return Json(response);
            }

            return View();
        }

        [HttpPost]
        public ActionResult CariHareketListesi(int cariId, DateTime? ilkTarih, DateTime? sonTarih)
        {
            var result = service.CariHareketListesi(cariId, ilkTarih, sonTarih);

            return Json(result);
        }
    }
}