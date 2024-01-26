using DataIslemler.Models;
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
    public class IcisleriTebligatController : BaseController
    {
        private readonly IVirtualContext vctx;
        private readonly IcisleriService service;
        public IcisleriTebligatController(IVirtualContext _vctx, IcisleriService _service)
        {
            vctx = _vctx;
            service = _service;
        }
        // GET: IcisleriTebligat
        public ActionResult TebligatListesi([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {
                var liste = service.GetTebligatListesi("Okunmadı");

                var res = liste.ToDataSourceResult(req);

                return Json(res);
            }
            return View();
        }

        public ActionResult SorguTebligatListesi([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {
                var _tarih = this.Request.QueryString["tarih"];
                var tarih = Convert.ToDateTime(_tarih);
                var liste = service.SorguGetTebligatListesi("",tarih);

                var res = liste.ToDataSourceResult(req);

                return Json(res);
            }
            return View();
        }

        public ActionResult PdfDokumanGetir(int? id)
        {

            var pdfFile = service.GetPdfDokuman(id);

            if (pdfFile!=null)
            {
                return FileStreamResult(pdfFile.Icerik, "application/pdf", pdfFile.DosyaAdi);
            }

            return Content("Pdf bulunamadı");
            
        }

       
        public ActionResult EkZipIndir(int? tebligatId,string tur)
        {
            var file = service.EkZipIndir(tebligatId, tur);
            var dosya = File(file.file, "application/zip", file.fileName);

            return dosya;
        }

        [HttpPost]
        public ActionResult OkumaDurumGuncelle(int? tebligatId)
        {

            service.OkumaDurumGuncelle(tebligatId);

            return Json(true);
        }

        [HttpPost]
        public ActionResult TopluOkumaDurumGuncelle(List<int> tebligatListesi)
        {
            service.TopluOkumaDurumGuncelle(tebligatListesi);
            return Json(true);
        }

        [HttpPost] 
        public ActionResult TebligatDataSorguIslem(ListeleRequestModel clienSorguModel)
        {

            //var liste = service.TebligatDataSorguIslem(clienSorguModel);

            return Json(clienSorguModel);
        }

        [HttpPost]
        public ActionResult MaliyeListeKayitKontrolIslem(List<IsisleriResponseListe> clientListe)
        {
            var result = service.MaliyeListeKayitKontrolIslem(clientListe);

            return Json(result);
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
        public ActionResult GetTebligatListesi(string durum)
        {

            var result = service.GetTebligatListesi(durum);

            return Json(result);
        }

        [HttpPost]
        public ActionResult TebligatSorguIslem(List<Firma> firmaListesi)
        {
            var result = service.TebligatSorguIslem(firmaListesi);

            return Json(result);
        }
    }
}