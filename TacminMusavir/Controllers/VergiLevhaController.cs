using Kendo.Mvc.UI;
using System.Diagnostics;
using System.Web.Mvc;
using Tacmin.Core;
using Tacmin.Core.Interface;

namespace TacminMusavir.Controllers
{
    public class VergiLevhaController : BaseController
    {


        private readonly IVirtualContext vctx;
        private readonly Tacmin.Service.Maliye.VergiLevhaService vergiLevhaService;

        public VergiLevhaController(IVirtualContext vctx, Tacmin.Service.Maliye.VergiLevhaService vergiLevhaService)
        {

            this.vctx = vctx;
            this.vergiLevhaService = vergiLevhaService;

        }

        public ActionResult VergiLevhaListe([DataSourceRequest] DataSourceRequest req)
        {
            if (Request.IsAjaxRequest())
            {
                var res = vergiLevhaService.GetVergiLevhaListesi<DataIslemler.Models.Mukellef.VergiLevhaListeModel>(req);

                return Json(res);
            }
            return View();
        }

        [HttpPost]
        public ActionResult VergiLevhasiSorgula(DataIslemler.Models.Mukellef.VergiLevhaRequestModel fatura_bilgi)
        {

            var sw = new Stopwatch();
            sw.Start();
            // vergi levhalarını indir
            vergiLevhaService.VergiLevhasiIslem(fatura_bilgi);
            sw.Stop();


            return View();

        }
        // GET: VergiLevhaListesi
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LevhaPdfGor(int? id)
        {
            if (id != null)
            {
                var file = vergiLevhaService.LevhaPdfGor(id);
                if (file.file.Length > 0)
                {
                    return FileStreamResult(file.file, "application/pdf", file.fileName);
                }
            }

            return Content("Pdf bulunamadı");
        }
    }
}