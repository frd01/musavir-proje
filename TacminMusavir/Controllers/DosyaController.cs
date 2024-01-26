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
    public class DosyaController : BaseController
    {
        private readonly IVirtualContext vctx;
        private readonly DosyaService service;

        public DosyaController(IVirtualContext _vctx, DosyaService _service)
        {
            vctx = _vctx;
            service = _service;
        }
        // GET: Dosya
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult KurulumDosyaIndir()
        {

            var file = service.KurulumDosyasiIndir();

            var dosya = File(file.file, "application/zip", file.fileName);

            return dosya;
        }

        public ActionResult MukellefBilgiGuncellemeExcelIndir()
        {
            var file = service.MukellefBilgiGuncellemeExcelIndir();

            

            var dosya = File(file.file, "application/zip", file.fileName);

            return dosya;
        }


    }
}