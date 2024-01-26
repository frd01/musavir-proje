using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tacmin.Core;
using Tacmin.Core.Interface;
using Tacmin.Model.Tebligat;
using Tacmin.Model.Whatsapp;
using Tacmin.Service.Tebligat;

namespace TacminMusavir.Controllers
{
    public class VergiDenetimController : BaseController
    {
        private readonly IVirtualContext vctx;
        private readonly VergiDenetimService service;

        public VergiDenetimController(IVirtualContext _vctx, VergiDenetimService _service)
        {
            vctx = _vctx;
            service = _service;
        }
        // GET: VergiDenetim
        public ActionResult TebligatListesi()
        {
            return View();
        }

        public ActionResult SorguTebligatListesi([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {
                var _tarih = this.Request.QueryString["tarih"];
                var tarih = Convert.ToDateTime(_tarih);

                var liste = service.SorguGetTebligatListesi("", tarih);

                var response = liste.ToDataSourceResult(req);

                return Json(response);
            }

            return View();
        }

        [HttpPost]
        public ActionResult MaliyeListeKayitKontrolIslem(List<VergiDenetimMlyListeModel> clientList)
        {
            var result = service.MaliyeListeKayitKontrolIslem(clientList);

            return Json(result);
        }

        [HttpPost]
        public ActionResult GetTebligatListesi(string durum)
        {
            var result = service.GetTebligatListesi(durum);

            return Json(result);
        }

        public ActionResult PdfDokumanGoster(int? tebligatId)
        {
            var file = service.PdfDokumanGoster(tebligatId);

            if (file != null)
            {
                return FileStreamResult(file.Icerik, "application/pdf", file.DosyaAdi);
            }

            return Content("Pdf bulunamadı");
        }

        public ActionResult EkZipIndir(int? tebligatId, string tur)
        {
            var file = service.EkZipIndir(tebligatId, tur);
            var dosya = File(file.file, "application/zip", file.fileName);

            return dosya;
        }

        [HttpPost]
        public ActionResult OkunduDurumGuncelle(List<int> clientList)
        {
            service.OkunduDurumGuncelle(clientList);

            return Json(true);
        }
        [HttpPost]
        public ActionResult WhatsAppGonderimIcerikOlustur(List<TebligatDataListModel> clientList)
        {

            var result = service.WhatsAppGonderimIcerikOlustur(clientList);

            return Json(result);
        }

        [HttpPost]
        public ActionResult WhatsappGonderimDataKayit(List<FirmaModel> clientList)
        {

            service.WhatsappGonderimDataKayit(clientList);

            return Json(true);
        }
    }
}