using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tacmin.Core;
using Tacmin.Core.Interface;
using Tacmin.Model.Musavir;
using Tacmin.Service;

namespace TacminMusavir.Controllers
{
    public class MusavirController : BaseController
    {

        private readonly IVirtualContext vctx;
        private readonly MusavirService service;

        public MusavirController(IVirtualContext _vctx, MusavirService _service)
        {
            vctx = _vctx;
            service = _service;
        }

        // GET: Musavir
        public ActionResult Sozlesmeler()
        {
            return View();
        }

        public ActionResult MukellefBorcSorgula()
        {

            return View();
        }

        [HttpGet]
        public ActionResult GetMusavirBilgi()
        {
            var result = service.GetMusavirBilgi();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult VergiTurListesi()
        {
            var result = service.VergiTurListesi();

            return Json(result,JsonRequestBehavior.AllowGet);
        }

      
    }
}