using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Tacmin.Core;
using Tacmin.Core.Interface;
using Tacmin.Model.Main;
using Tacmin.Service;

namespace TacminMusavir.Controllers
{
    public class MainController : BaseController
    {

        private readonly MainService mainService;
        private readonly IVirtualContext vctx;


        public MainController(IVirtualContext _vctx,MainService _mainService )
        {
            try
            {
                mainService = _mainService;
                vctx = _vctx;
               
            }
            catch (System.Exception ex)
            {

                throw;
            }
            
        }
       
        [Yetki(nameof(Yetkiler.DASHBORD))]
        public ActionResult Index()
        {
            //var data_adi = Request.Cookies.Get("data_adi").Value.ToString();
            //mainService.DataAdi = data_adi;
            return View();
        }

        /*[HttpGet]
        public ActionResult BeyannameTakipListesi()
        {

            var liste = new DataIslemler.Listeler.Mukellef.BeyannameTakipTabloListesi(vctx.DataAdi).GetBeyannameTabloTakipListesi();

            return Json(liste);
        }*/

        [HttpPost]
        public ActionResult TakipDetay(List<Tacmin.Model.Mukellef.TakipFirma> firma_listesi)
        {

            return PartialView("TakipDetay");
        }

        public ActionResult TakipDetay([DataSourceRequest] DataSourceRequest req)
        {

            var liste = new List<Tacmin.Model.Mukellef.TakipFirma>();

            var model = new Tacmin.Model.Mukellef.TakipFirma();
            model.Id = 1;
            model.Unvan = "Ferdi İnan";
            liste.Add(model);

            var result = liste.ToDataSourceResult(req);

            return Json(result);
        }

        [HttpGet]
        public ActionResult LocalListeler(){

            var result = mainService.LocalListeler();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetGibFirmaListesi()
        {
            var result = mainService.GetGibFirmaListesi();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ServisCalistir()
        {
            var path = @"C:\local-tacminsoft\servisler\MusavirMasaUstuServis\MusavirMasaUstuServis.exe";
            System.Diagnostics.Process.Start(path);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}