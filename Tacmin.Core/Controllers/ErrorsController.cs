using System.Collections.Generic;
using System.Web.Mvc;
using Tacmin.Core.Model;

namespace Tacmin.Core.Controllers
{
    public class ErrorsController : BaseController
    {
        public ActionResult Http500(ErrorViewModel error)
        {
            Response.StatusCode = 500;
            return View(error);
        }

        public ActionResult Http404(ErrorViewModel error)
        {
            Response.StatusCode = 404;
            return View(error);
        }

        public ActionResult YetkisizErisim(IEnumerable<string> gerekenYetkiler)
        {
            Response.StatusCode = 403;
            return View(gerekenYetkiler);
        }
    }
}
