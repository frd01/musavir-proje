using System.Web.Mvc;
using Tacmin.Core;
using Tacmin.Core.Attributes;
using Tacmin.Core.Interface;
using Tacmin.Model;
using Tacmin.Service;

namespace TacminMusavir.Controllers
{
    public class HesapController : BaseController
    {
        private readonly HesapService _hesapService;
        private readonly IVirtualContext _vctx;

        public HesapController(
            HesapService hesapService, IVirtualContext vctx
            )
        {
            _hesapService = hesapService;
            _vctx = vctx;
        }

        //[RestoreModelState]
        public ActionResult Hesabim()
        {
            var model = _hesapService.GetUser();

            return View(model);
        }

        [HttpPost]
        public ActionResult KullaniciBilgiGuncelle(Tacmin.Model.Hesap.KullaniciModel kullanici)
        {
            var result = _hesapService.KullaniciBilgiGuncelle(kullanici);

            return Json(result);
        }

        [HttpGet]
        public JsonResult SmsFirmaListesi()
        {
            var result = _hesapService.SmsFirmaListesi();

            return Json(result, JsonRequestBehavior.AllowGet);
        }



    }
}