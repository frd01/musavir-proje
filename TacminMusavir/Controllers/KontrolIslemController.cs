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
    public class KontrolIslemController : BaseController
    {

        private readonly IVirtualContext vctx;
        private readonly KontrolIslemService service;

        public KontrolIslemController(IVirtualContext _vctx, KontrolIslemService _service)
        {
            vctx = _vctx;
            service = _service;
        }

        // GET: KontrolIslem
        public ActionResult FirmaGibKontrolListesi()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FirmaKontrolListesi()
        {

            var result = service.FirmaKontrolListesi();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}