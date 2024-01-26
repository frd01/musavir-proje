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
    public class IslemController : BaseController
    {
        private readonly IVirtualContext vctx;
        private readonly IslemService service;

        public IslemController(IVirtualContext _vctx, IslemService _service)
        {
            vctx = _vctx;
            service = _service;
        }
        // GET: Islem
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult KullaniciGirisKaydi()
        {
            service.KullaniciGirisKaydi();

            return Json(true);
        }
    }
}