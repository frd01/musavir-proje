using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using Tacmin.Core.Model;

namespace Tacmin.Core
{
    public class BaseController : Controller
    {
        public static YetkilerModel Yetkiler = new YetkilerModel();

        public BaseController()
        {

        }

        public JsonResult JsonSuccess(object data)
        {
            var model = new JsonSuccessModel
            {
                success = true,
                data = data
            };

            return Json(model);
        }

        public JsonResult JsonError(string msg)
        {
            var model = new JsonErrorModel
            {
                success = false,
                errorMessage = msg
            };

            return Json(model);
        }

        public new ActionResult File(byte[] arr, string contentType, string fileName)
        {
            //var dosya_adi = "beyannameler.zip";
            //Response.AddHeader("Content-Disposition", "filename=\"" + fileName + "\"");
            //Response.AddHeader("Content-Disposition", "attachment; filename=\"file," + fileName + "\"");
            return base.File(arr, contentType, fileName);
        }

        public ActionResult FileStreamResult(byte[] arr, string contentType, string fileName)
        {
            Response.AddHeader("Content-Disposition", "filename=\"" + fileName + "\"");

            var pdfStream = new MemoryStream();
            pdfStream.Write(arr, 0, arr.Length);
            pdfStream.Position = 0;

            return new FileStreamResult(pdfStream, contentType);
        }

        //protected new RedirectToRouteResult RedirectToRoute(RouteValueDictionary routeValues)
        //{
        //    var qs = new NameValueCollection(Request.Unvalidated.QueryString);

        //    if (Request.UrlReferrer != null)
        //        qs.Add(HttpUtility.ParseQueryString(Request.UrlReferrer.Query));

        //    //if (AsEngine.Config.persist_query_strings.Count > 0)
        //    //{
        //    //    foreach (string item in AsEngine.Config.persist_query_strings)
        //    //    {
        //    //        var val = qs.GetValues(item)?.FirstOrDefault();
        //    //        if (val != null && !routeValues.ContainsKey(item))
        //    //        {
        //    //            routeValues.Add(item, val);
        //    //            continue;
        //    //        }
        //    //    }
        //    //}

        //    return new RedirectToRouteResult(routeValues: routeValues);
        //}

        //protected new RedirectToRouteResult RedirectToRoute(object routeValues)
        //{
        //    return RedirectToRoute(new RouteValueDictionary(routeValues));
        //}

        #region Toast

        public void Toast(string message, ToastrType tip = ToastrType.Info)
        {
            Toast(string.Empty, message, tip);
        }

        public void Toast(string baslik, string message, ToastrType tip = ToastrType.Info)
        {
            var newmessage = new Toastr(message, baslik, tip);
            if (TempData["toastr_messages"] == null)
                TempData["toastr_messages"] = new List<Toastr>();
            ((List<Toastr>)TempData["toastr_messages"]).Add(newmessage);
        }

        #endregion Toast

        #region SweetAlert2

        public void SweetAlert(SweetAlert2 sweetAlert2)
        {
            TempData["sweetAlert_Messages"] = sweetAlert2.SweetAlert();
        }
        #endregion
    }
}
