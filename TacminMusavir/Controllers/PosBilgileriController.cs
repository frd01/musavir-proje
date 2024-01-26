using System.Web.Mvc;
using Tacmin.Core;
using Tacmin.Core.Interface;

namespace TacminMusavir.Controllers
{
    public class PosBilgileriController : BaseController
    {

        private readonly IVirtualContext vctx;

        public PosBilgileriController(IVirtualContext vctx)
        {
            this.vctx = vctx;
        }
        // GET: PosBilgileri
        public ActionResult Index()
        {
            return View();
        }
    }
}