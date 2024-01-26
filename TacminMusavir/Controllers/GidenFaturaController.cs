using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tacmin.Core;
using Tacmin.Core.Interface;
using Tacmin.Model.EArsiv;
using Tacmin.Model.Mukellef;
using Tacmin.Model.Ortak;
using Tacmin.Service;

namespace TacminMusavir.Controllers
{
    public class GidenFaturaController : BaseController
    {

        private readonly IVirtualContext vctx;
        private readonly GidenFaturaService service;

        public GidenFaturaController(IVirtualContext _vctx, GidenFaturaService _service)
        {

            vctx = _vctx;
            service = _service;
        }

        // GET: GidenFatura
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FaturaListesi()
        {
            var model = new TarihModel();

            //var ay = service.GetAyListesi(null)[0];

            //model.IlkTarih = ay.IlkTarih;
            //model.SonTarih = ay.SonTarih;

            return View(model);
        }

        public ActionResult SorguFaturaListesi([DataSourceRequest] DataSourceRequest req)
        {
            var model = new TarihModel();

            if (Request.IsAjaxRequest())
            {
                var _tarih = this.Request.QueryString["tarih"];
                var tarih = Convert.ToDateTime(_tarih);

                var liste = service.SorguFaturaListesi(tarih);

                var response = liste.ToDataSourceResult(req);

                return Json(response);
            }


            return View(model);
        }

        [HttpPost]
        public ActionResult FaturaKayitIslem(List<ArsivFaturaModel> clientFaturaListesi)
        {

            var result = service.FaturaKayitIslem(clientFaturaListesi); 

            return Json(result);
        }

        [HttpPost]
        public ActionResult GetFaturaListesi(List<FirmaGibModel> firmaListesi, DateTime? ilkTarih, DateTime? sonTarih)
        {
            var liste = service.GetFaturaListesi(firmaListesi, ilkTarih, sonTarih);

            return Json(liste);
        }

        public ActionResult GetPdfFatura(int? id)
        {

            if (id != null)
            {

                var file = service.PdfDosyaAc(id);

                return FileStreamResult(file.file, "application/pdf", file.fileName);
            }

            return Content("Pdf Dosya Bulunamadı");
        }

        public ActionResult GetHtmlFatura(int? id)
        {

            if (id != null)
            {

                var file = service.HtmlDosyaAc(id);

                return FileStreamResult(file.file, "text/html", file.fileName);
            }

            return Content("Pdf Dosya Bulunamadı");
        }

        public ActionResult FaturaZipDownload(List<ArsivFaturaModel> faturaListesi,string tur)
        {

            var file = service.FaturaZipDosyaOlustur(faturaListesi, tur);

            var dosya = File(file.file, "application/zip", file.fileName);

            return dosya;
        }
    }
}