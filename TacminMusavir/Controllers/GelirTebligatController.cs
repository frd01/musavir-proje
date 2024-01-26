using DataIslemler.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tacmin.Core;
using Tacmin.Core.Interface;
using Tacmin.Model.Mukellef;
using Tacmin.Model.Tebligat;
using Tacmin.Model.Whatsapp;
using Tacmin.Service.Tebligat;

namespace TacminMusavir.Controllers
{
    public class GelirTebligatController : BaseController
    {
        private readonly IVirtualContext vctx;
        private readonly GelirTebService service;
        public GelirTebligatController(IVirtualContext _vctx, GelirTebService _service)
        {
            vctx = _vctx;
            service = _service;
        }
        // GET: GelirTebligat
        public ActionResult TebligatListesi()
        {
            return View();
        }
        public ActionResult SorguTebligatListesi([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {
                var tarih = this.Request.QueryString["tarih"];
                var _tarih = Convert.ToDateTime(tarih);
                var liste = service.SorguGetTebligatListesi("", _tarih);

                var response = liste.ToDataSourceResult(req);

                return Json(response);
            }
            return View();
        }




        [HttpPost]
        public ActionResult ListeKayitKontrolIslem(List<GelirMlyResponseModel> clientList)
        {
            var result = service.ListeKayitKontrolIslem(clientList);

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
            var pdf_file = service.GetDosya(tebligatId);

            if (pdf_file!= null)
            {
                return FileStreamResult(pdf_file.Icerik, "application/pdf", pdf_file.DosyaAdi);
            }

            return Content("Pdf bulunamadı");
        }

        [HttpPost]
        public ActionResult TebligatDurumGuncelle(List<int> clientList)
        {
            service.TebligatDurumGuncelle(clientList);

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

        [HttpPost]
        public ActionResult TebligatSorguIslem(List<Firma> firmaListesi)
        {
            var result = service.TebligatSorguIslem(firmaListesi);

            return Json(result);
        }


    }
}