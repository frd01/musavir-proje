using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using Tacmin.Core;
using Tacmin.Core.Attributes;
using Tacmin.Core.Interface;
using Tacmin.Data.DbModel;
using Tacmin.Model;
using Tacmin.Model.Maliye;
using Tacmin.Model.Mukellef;
using Tacmin.Service;

namespace TacminMusavir.Controllers
{
    public class MukellefController : BaseController
    {
        private readonly MukellefService _mukellefService;

        private readonly IVirtualContext vctx;

       

        public MukellefController(
            MukellefService mukellefService, IVirtualContext vctx
            )
        {
            _mukellefService = mukellefService;
            this.vctx = vctx;
        }

        [Yetki(nameof(Yetkiler.MUKELLEF_LIST_GORUNTULE))]
        public ActionResult MukellefListesi([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {
                var liste = new DataIslemler.Listeler.Mukellef.Listeler(vctx.DataAdi).GetMukellefListesi();
                var res = liste.ToDataSourceResult(req);
                return Json(res);
            }

            return View();
        }

        [Yetki(nameof(Yetkiler.MUKELLEF_LIST_GORUNTULE))]
        [RestoreModelState]
        public ActionResult MukellefKayit(int? id)
        {
            var model = new MukellefKayitModel();
            
            if (id != null)
            {
                //model = _mukellefService.GetMukellef(id);
                model = new DataIslemler.Listeler.Mukellef.Listeler(vctx.DataAdi).GetMukellef(id.Value);
            }

            if (model.ID == null)
                model.AKTIF = true;

            model = TempData["ModelState"] as MukellefKayitModel ?? model;

            return View(model);
        }

        public ActionResult BankaHacizSorgulama()
        {

            return View();
        }

        public ActionResult AracHacizSorgulama()
        {

            return View();
        }

        [HttpPost]
        //[Yetki(nameof(Yetkiler.MUKELLEF_KAYIT_GUNCELLE))]
        //[SetModelState]
        public ActionResult MukellefKayitUpdate(MukellefKayitModel model, List<Firma_Iletisim> iletisim_listesi, BeyannameTakipModel firma_takip_bilgi)
        {
            
           
            if (model.ID == null)
            {
                //model.ID = _mukellefService.AddMukellef(model);
                model.ID = new DataIslemler.Islemler.Mukellef.DataIslem(vctx.DataAdi).YeniMukellefKaydet(model);
                firma_takip_bilgi.Id = model.ID.Value;
                
                new DataIslemler.Islemler.Mukellef.BeyannameTakipIslem(vctx.DataAdi).MukellefTakipBilgiKayit(firma_takip_bilgi);
                return Json(true);
            }
            else
            {
                //_mukellefService.UpdateMukellef(model);

                new DataIslemler.Islemler.Mukellef.DataIslem(vctx.DataAdi).MukellefGuncelle(model);
                    
                if (iletisim_listesi != null)
                {
                    new DataIslemler.Islemler.Mukellef.IletisimBilgiIslem(vctx.DataAdi).ListeKayit(iletisim_listesi, model.ID);
                }
                firma_takip_bilgi.Id = model.ID.Value;
                new DataIslemler.Islemler.Mukellef.BeyannameTakipIslem(vctx.DataAdi).MukellefTakipBilgiKayit(firma_takip_bilgi);

                ViewBag.MukellefId = null;
                return Json(true);
            }
        }

        [HttpPost]
        [Yetki(nameof(Yetkiler.MUKELLEF_KAYIT_SIL))]
        public JsonResult MukellefDestroy(int? id)
        {
            var mukellef = _mukellefService.GetMukellef(id);
            if (mukellef != null)
            {
                _mukellefService.DestroyMukellef(id.Value);
                return JsonSuccess("Mükellef kartı silindi");
            }

            return JsonError("Mükellef kaydı bulunamadı");
        }

        [Yetki(nameof(Yetkiler.MUKELLEF_KAYIT_GOR))]
        public JsonResult MukellefFaaliyetKodlari([DataSourceRequest] DataSourceRequest req, int? firmaId)
        {
            var res = _mukellefService.FirmaFaaliyetKodlari(req, firmaId);
            return Json(res);
        }

        [Yetki(nameof(Yetkiler.MUKELLEF_KAYIT_GOR))]
        public JsonResult MukellefSubeTainmlari([DataSourceRequest] DataSourceRequest req, int? firmaId)
        {
            var res = _mukellefService.FirmaSubeTanimlari(req, firmaId);
            return Json(res);
        }

        [HttpPost]
        [Yetki(nameof(Yetkiler.MUKELLEF_KAYIT_GUNCELLE))]
        public JsonResult MukellefSubeAddOrUpdate([DataSourceRequest] DataSourceRequest req, FirmaSubeKayitModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ID == null)
                {
                    model.ID = _mukellefService.MukellefSubeAdd(model);
                }
                else
                {
                    _mukellefService.MukellefSubeUpdate(model);
                }
            }

            return Json(new[] { model }.ToDataSourceResult(req, ModelState));
        }

        [HttpPost]
        [Yetki(nameof(Yetkiler.MUKELLEF_KAYIT_SIL))]
        public JsonResult MukellefSubeDestroy([DataSourceRequest] DataSourceRequest req, FirmaSubeKayitModel model)
        {
            _mukellefService.MukellefSubeDestroy(model);
            return Json(new[] { model }.ToDataSourceResult(req, ModelState));
        }

        public ActionResult BeyannameTakipListesi()
        {
            var liste = new DataIslemler.Listeler.Mukellef.BeyannameDonemListesi(vctx.DataAdi).DonemListesi();
            ViewData["beyannameDonemListesi"] = liste;

            return View();
        }

        [HttpGet]
        public ActionResult GetBeyannameTakipList()
        {

            var takip_listesi = new DataIslemler.Listeler.Mukellef.BeyannameTakip(vctx.DataAdi).BeyannameTakipListesi();

            return Json(takip_listesi);
        }

        [HttpGet]
        public ActionResult MaliyeBilgiGuncelleme()
        {

            var gibService = new GibAuthService();

            var result_model = gibService.IvdLogin(vctx.GibBilgileri.kullaniciKodu, vctx.GibBilgileri.sifre);

            var sw = new Stopwatch();
            sw.Start();

            var result = _mukellefService.MaliyeBilgiGuncelleme(result_model.Token);
            gibService.IvdCikisYap(result_model.Token);
            sw.Stop();

            return Json(result);
        }

        [HttpPost]
        public ActionResult TakipListesiGuncelle(List<BeyannameTakipModel> beyanname_takip_listesi)
        {
            if (beyanname_takip_listesi != null)
            {
                new DataIslemler.Islemler.Mukellef.BeyannameTakipIslem(vctx.DataAdi).TakipListesiKayit(beyanname_takip_listesi);
            }
            return Json(true);
        }

        [HttpPost]
        public ActionResult BankaHacizListeVergiDairesiGuncelle(List<BankaHacizModel> clientList)
        {

            var result = _mukellefService.BankaHacizListeVergiDairesiGuncelle(clientList);

            return Json(result);
        }
        [HttpPost]
        public ActionResult AracHacizListeGuncelle(List<AracHacizModel> clientList)
        {
            var result = _mukellefService.AracHacizListeGuncelle(clientList);

            return Json(result);
        }

        [ChildActionOnly]
        public ActionResult MukellefFirmaTakip(int? id)
        {

            var model = new BeyannameTakipModel();

            var model_islem = new DataIslemler.Islemler.Mukellef.MukellefDetayFirmaTakip(vctx.DataAdi);

            model = model_islem.GetMukellefFirmaBilgiModel(id);

            return PartialView(model);
        }

        public JsonResult BeyannameDonemListesi()
        {

            var liste = new DataIslemler.Listeler.Mukellef.BeyannameDonemListesi(vctx.DataAdi).DonemListesi();

            return Json(liste);
        }

        public ActionResult FirmaBeyannameDurumListesi()
        {
            var model = new DataIslemler.Listeler.Genel().MukellefAyModel(DateTime.Now.Month);

            ViewBag.AyBaslik = model.Title;

            return View(model);
        }

        [HttpPost]
        public ActionResult Get_FirmaBeyannameDurumListesi(int? ay)
        {
            var ay_value = DateTime.Now.Month;
            if (ay != null)
            {
                ay_value = ay.Value;
                var model = new DataIslemler.Listeler.Genel().MukellefAyModel(DateTime.Now.Month);
                ViewBag.AyBaslik = model.Title;
            }
            var liste = new DataIslemler.Listeler.Mukellef.FirmaBeyannameTakipListesi(ay_value, vctx.DataAdi).Firma_Beyanname_Takip_Listesi();

            return Json(liste);
        }

        [HttpGet]
        public ActionResult Get_AyListesi()
        {

            var liste = new DataIslemler.Listeler.Genel().AyListesi();

            return Json(liste);
        }

        [HttpPost]
        public ActionResult FirmaIletisimListesi(int firmaId)
        {
            var result = _mukellefService.FirmaIletisimListesi(firmaId);

            return Json(result);
        }

        public JsonResult SehirListesi()
        {

            var liste = new DataIslemler.Listeler.OrtakListeler(vctx.DataAdi).SehirListesi();

            return Json(liste);
        }

        public JsonResult IlceListesi()
        {

            var liste = new DataIslemler.Listeler.OrtakListeler(vctx.DataAdi).IlceListesi();

            return Json(liste);
        }


        public JsonResult VergiDaireListesi()
        {

            var liste = new DataIslemler.Listeler.OrtakListeler(vctx.DataAdi).VergiDaireListesi();

            return Json(liste);
        }
        [HttpPost]
        public ActionResult Get_IlceListesi(int sehir_id)
        {

            var liste = new DataIslemler.Listeler.OrtakListeler(vctx.DataAdi).Get_IlceListesi(sehir_id);

            return Json(liste);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, Tacmin.Data.DbModel.Firma_Iletisim iletisim)
        {
           
            return Json(new[] { iletisim }.ToDataSourceResult(request, ModelState));
        }

        [HttpGet]
        public ActionResult MukellefGibGuncellemeExcelListesi()
        {
            var result = _mukellefService.MukellefGibGuncellemeExcelListesi();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult MukellefGibBilgiGuncelle()
        {
            if (Request.Files.Count > 0)
            {
                var files = Request.Files;

                var file = files[0];

                var result = _mukellefService.MukellefGibBilgiGuncelleme(file.InputStream);

                return Json(result);
            }

            return Json(null);
        }
        [HttpPost]
        public ActionResult MusteriSgkSubeListesi(int? firmaId)
        {
            var result = _mukellefService.MusteriSgkSubeListesi(firmaId);

            return Json(result);
        }
        [HttpPost]
        public ActionResult  BilgiTakipList(int? subeId)
        {
            var result = _mukellefService.BilgiTakipList(subeId);

            return Json(result);
        }
        [HttpPost]
        public ActionResult MukellefSgkSubeKaydet(SgkListeModel clientData, int? firmaId)
        {
            var result = _mukellefService.MukellefSgkSubeKaydet(clientData, firmaId);
            return Json(result);
        }
    }

    public class BeyannameTakipData
    {

        public List<BeyannameTakipModel> beyanname_takip_listesi { get; set; }
    }



   
}