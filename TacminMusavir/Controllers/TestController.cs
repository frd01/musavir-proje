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

    public class TestController : BaseController
    {

        private readonly IVirtualContext vctx;
        private readonly TestService service;

        public TestController(IVirtualContext _vctx, TestService _service)
        {

            vctx = _vctx;
            service = _service;

        }
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetToken(string gibKodu,string gibSifre)
        {

            var result = service.GetToken(gibKodu, gibSifre);

            return Json(result);
        }
    }
}