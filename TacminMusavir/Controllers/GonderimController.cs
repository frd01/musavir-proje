using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tacmin.Core.Interface;
using Tacmin.Model;
using Tacmin.Model.Mail;
using Tacmin.Model.Sms;
using Tacmin.Service;

namespace TacminMusavir.Controllers
{
    public class GonderimController : Controller
    {
        private readonly IVirtualContext vctx;
        private readonly GonderimService service;

        public GonderimController(IVirtualContext _vctx, GonderimService _service)
        {
            vctx  = _vctx;
            service = _service;
        }
        // GET: Gonderim
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult MailBilgi()
        {
            var result = service.MailBilgi();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult BeyannaneGonderimIcerikOlustur(List<BeyannameAraModel> clientList, string gonderimTur)
        {
            var result = service.SmsBeyannaneGonderimIcerikOlustur(clientList, gonderimTur);

            return Json(result);
        }
        [HttpPost]
        public ActionResult MailDataKayitIslem(List<MailResModel> islemListesi,int modul,List<BeyannameAraModel> clientList)
        {
            var result = service.MailDataKayitIslem(islemListesi,modul,clientList);

            return Json(result);
        }
        [HttpPost]
        public ActionResult MailBeyannaneGonderimIcerikOlustur(List<BeyannameAraModel> clientList, string gonderimTur)
        {
            var result = service.MailBeyannaneGonderimIcerikOlustur(clientList, gonderimTur);

            return Json(result);
        }

        [HttpPost]
        public ActionResult SmsDataKayitIslem(List<SmsResModel> islemListesi,int modul,List<BeyannameAraModel> clientList)
        {
            var result = service.SmsDataKayitIslem(islemListesi,modul,clientList);

            return Json(result);
        }

        [HttpGet]
        public ActionResult SmsBilgi ()
        {
            var result = service.SmsBilgi();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult MailBildirgeGonderimIcerikOlustur(List<BildirgeAraModel> clientList, string gonderimTur)
        {
            var result = service.MailBildirgeGonderimIcerikOlustur(clientList, gonderimTur);

            return Json(result);
        }

        [HttpPost]
        public ActionResult SmsBildirgeGonderimIcerikOlustur(List<BildirgeAraModel> clientList, string gonderimTur)
        {
            var result = service.SmsBildirgeGonderimIcerikOlustur(clientList, gonderimTur);

            return Json(result);
        }
    }
}