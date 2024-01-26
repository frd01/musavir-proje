using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.Mvc;
using Tacmin.Core;
using Tacmin.Core.Interface;
using Tacmin.Model;
using Tacmin.Model.MaliyeIslemleri.Beyanname;
using Tacmin.Model.Ortak;
using Tacmin.Model.Whatsapp;
using Tacmin.Service;

namespace TacminMusavir.Controllers
{
    public class BeyannameController : BaseController
    {
        private readonly GibAuthService _gibAuth;
        private readonly BeyannameService _beyannameService;
        private readonly IVirtualContext _vctx;

        public BeyannameController(
            GibAuthService gibAuth,
            BeyannameService beyannameService,
            IVirtualContext vctx
            )
        {
            _gibAuth = gibAuth;
            _beyannameService = beyannameService;
            _vctx = vctx;
            
            
        }

        [Yetki(nameof(Yetkiler.BEYANNAME_LIST_GORUNTULE))]
        public ActionResult BeyannameListesi([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {
                var liste = new DataIslemler.Listeler.Beyanname.BeyannameListesi(_vctx.DataAdi).GetBeyannameListesi();

                var res = liste.ToDataSourceResult(req);

                return Json(res);
            }

            return View();
        }

        public ActionResult SorguBeyannameListesi([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {
                var _tarih = this.Request.QueryString["tarih"];
                var tarih = Convert.ToDateTime(_tarih);
                var liste = new DataIslemler.Listeler.Beyanname.BeyannameListesi(_vctx.DataAdi).GetBeyannameListesi(tarih);

                var res = liste.ToDataSourceResult(req);

                return Json(res);
            }

            return View();
        }

        public ActionResult MevcutAracListesi()
        {
            return View();
        }

        public ActionResult EskiAracListesi()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetBeyannameListesi()
        {

            var liste = new DataIslemler.Listeler.Beyanname.BeyannameListesi(_vctx.DataAdi).GetBeyannameListesi();

            return Json(liste);
        }

        [HttpPost]
        public ActionResult GetBeyannameDataAramaListe(BeyannameDataRequestModel client_data)
        {

            try
            {
                if(client_data.Cariler == null)
                {
                    client_data.Cariler = new List<int>();
                }

                if(client_data.BeyannameTurler == null)
                {
                    client_data.BeyannameTurler = new List<string>();
                }
                var liste = new DataIslemler.Listeler.Beyanname.BeyannameListesi(_vctx.DataAdi).GetDataSorguBeyannameListesi(client_data);

                return Json(liste);
            }
            catch (System.Exception ex)
            {

                return Json(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetToken()
        {

            var kullanici = new DataIslemler.Islemler.GibBilgiIslem(_vctx.DataAdi).GetGibBilgi(_vctx.UserKodu);

            var result = _gibAuth.IvdLogin(kullanici.GibKodu, kullanici.GibSifre);

            return Json(result);
        }


        [HttpPost]
        //[Yetki(nameof(Yetkiler.BEYANNAME_SORGULA))]
        public ActionResult GibBeyannameSorgula(BeyannameRequestModel client_veri,bool is_aktif_cari)
        {

            try
            {
                

                var kullanici = new DataIslemler.Islemler.GibBilgiIslem(_vctx.DataAdi).GetGibBilgi(_vctx.UserKodu);

                

                client_veri.VergiTanim = "0";
                client_veri.TcTanim = "0";
                client_veri.BeyannameTurTanim = "0";
                client_veri.VergiDairesiTanim = "0";

                if (client_veri == null)
                {
                    client_veri = new BeyannameRequestModel();

                    client_veri.VergiTanim = "0";
                    client_veri.TcTanim = "0";
                    client_veri.BeyannameTurTanim = "0";
                    client_veri.VergiDairesiTanim = "0";

                    client_veri.Is_Vergi_No = false;
                    client_veri.Is_Vergi_Dairesi = false;
                    client_veri.Is_Vergi_Tur = false;
                    client_veri.TARIH_SON = DateTime.Now;
                    client_veri.TARIH_ILK = client_veri.TARIH_SON.Value.AddDays(-7);
                }

                if (client_veri.Is_Vergi_Tur == true)
                {

                    client_veri.BeyannameKodu = new DataIslemler.Islemler.Beyanname.BeyannameIslem(_vctx.DataAdi).Get_Beyanname_Kodu(client_veri.Vergi_Tur_Id.Value);
                }

                var sw = new Stopwatch();
                //sw.Start();

                var tarih_listesi = _beyannameService.BeyannameTarihListesi(client_veri.TARIH_ILK, client_veri.TARIH_SON);

                var model = _beyannameService.BeyannameOzet;

                var service = new ServiceIslem();
                var girisIslem = new MaliyeGirisIslem(service);

                var token_islem = girisIslem.IvdGirisYap(kullanici.GibKodu, kullanici.GibSifre);



                if (token_islem.messages != null)
                {
                    var text = token_islem.messages[0].text;

                    model.IslemDurum = false;
                    model.Mesaj = text;

                    return Json(model);

                    /*while (text == "Doğrulama hatası Lütfen doğrulama kodunu kontrol ediniz.")
                    {
                        System.Threading.Thread.Sleep(3000);
                        token_islem = girisIslem.IvdGirisYap(kullanici.GibKodu, kullanici.GibSifre);
                        text = "";
                        if (token_islem.messages != null)
                        {
                            text = token_islem.messages[0].text;
                        }
                    }*/
                    /*if (text != "Doğrulama hatası Lütfen doğrulama kodunu kontrol ediniz.")
                    {
                        model.IslemDurum = false;
                        model.Mesaj = text;

                        return Json(model);
                    }*/
                }

                try
                {
                    var ilkTarih = client_veri.TARIH_ILK.Value.ToString("yyyyMMdd");
                    var sonTarih = client_veri.TARIH_SON.Value.ToString("yyyyMMdd");
                    var indirilenBeyannameAdet = _beyannameService.GibBeyannameSorgula(token_islem.Token, client_veri, is_aktif_cari, service, ilkTarih, sonTarih);

                    var isDurum = _beyannameService.BeyannameIndirmeKontrol();
                    girisIslem.IvdCikisYap(token_islem.Token);
                }
                catch (Exception ex)
                {

                    girisIslem.IvdCikisYap(token_islem.Token);
                    model.IslemDurum = false;
                    model.Mesaj = ex.Message;

                    return Json(model);

                }

                return Json(_beyannameService.BeyannameOzet);
            }
            catch (System.Exception ex)
            {

                return Json(ex.ToString());
            }

           
        }

        [Yetki(nameof(Yetkiler.BEYANNAME_BEYANNAME_GORUNTULE))]
        public ActionResult BeyannamePdfGor(int? id)
        {
            if (id != null)
            {
                var file = _beyannameService.BeyannamePdfGor(id);
                if (file.file.Length > 0)
                {
                    return FileStreamResult(file.file, "application/pdf", file.fileName);
                }
            }

            return Content("Pdf bulunamadı");
        }

        public ActionResult BeyannamePdfIndir(int? id)
        {

            var file = _beyannameService.BeyannamePdfGor(id);

            var dosya = File(file.file, "application/pdf", file.fileName);

            return dosya;
        }

        [Yetki(nameof(Yetkiler.BEYANNAME_TAHAKKUK_GORUNTULE))]
        public ActionResult TahakkukPdfGor(int? id)
        {
            if (id != null)
            {
                var file = _beyannameService.TahakkukPdfGor(id);
                if (file.file.Length > 0)
                {
                    return FileStreamResult(file.file, "application/pdf", file.fileName);
                }
            }

            return Content("Pdf bulunamadı");
        }

        public ActionResult TahakkukPdfIndir(int? id)
        {
            var file = _beyannameService.TahakkukPdfGor(id);

            var dosya = File(file.file, "application/pdf", file.fileName);

            return dosya;

        }



        public ActionResult BeyannameZipDownload(List<BeyannameAraModel> beyanname_listesi, BeyannameBelgeModel belgeModel)
        {
            var file = _beyannameService.BeyannameZipOlustur(beyanname_listesi, belgeModel);
            var dosya = File(file.file, "application/zip", file.fileName);

            return dosya;
        }

        public ActionResult BeyannameTopluPdf(List<BeyannameAraModel> beyannameler, string pdftype)
        {
            var file = _beyannameService.BeyannameTopluPdfOlustur(beyannameler, pdftype);
            return File(file.file, "application/pdf", file.fileName);
        }

        [HttpPost]
        public ActionResult BeyannameSilme(SecilenBeyannameler beyanname_listesi)
        {
            var sw = new Stopwatch();
            sw.Start();
            new DataIslemler.Islemler.Beyanname.BeyannameIslem(_vctx.DataAdi).BeyannameSil(beyanname_listesi.Beyannameler);
            sw.Stop();

            return Redirect(Url.RouteUrl(new { controller = "beyanname", action = "BeyannameListesi" }));
        }

        [ChildActionOnly]
        public ActionResult BeyannameDataArama()
        {

            var model = new Tacmin.Model.MaliyeIslemleri.Beyanname.BeyannameListeleModel();

            return PartialView("Partial/BeyannameDataArama", model);
        }

        public JsonResult VergiTurListesi()
        {

            var liste = new DataIslemler.Listeler.MaliyeListeler(_vctx.DataAdi).VergiTurListesi();

            return Json(liste);
        }

        [HttpGet]
        public ActionResult GetVergiTurListesi()
        {

            var liste = new DataIslemler.Listeler.MaliyeListeler(_vctx.DataAdi).VergiTurListesi();

            return Json(liste);
        }

        [HttpPost]
        public ActionResult GonderimIcerikOlustur(List<BeyannameAraModel> clientList, string gonderimTur)
        {

            var result = _beyannameService.GonderimIcerikOlustur(clientList, gonderimTur);

            return Json(result);
        }

        [HttpPost]
        public ActionResult GonderimDataKayit(List<FirmaModel> clientList,List<BeyannameAraModel> beyannameListesi)
        {
           var result =  _beyannameService.GonderimDataKayit(clientList,beyannameListesi);

            return Json(result);
        }

        public JsonResult GibMukellefListesi()
        {

            var liste = new DataIslemler.Listeler.Mukellef.GibListeler(_vctx.DataAdi).FirmaBeyannameSorguListe();

            return Json(liste);
        }

        public JsonResult VergiDaireListesi()
        {

            var liste = new DataIslemler.Listeler.MaliyeListeler(_vctx.DataAdi).VergiDaireListesi();

            return Json(liste);
        }
        public JsonResult GetBeyannameTurGroupListe()
        {

            var liste = new DataIslemler.Listeler.Beyanname.Listeler(_vctx.DataAdi).GetBeyannameTurListesi();

            return Json(liste);
        }
    }

    public class SecilenBeyannameler
    {
        public List<int> Beyannameler { get; set; }
    }

    public class BeyannameData
    {

        public int id { get; set; }
    }
}