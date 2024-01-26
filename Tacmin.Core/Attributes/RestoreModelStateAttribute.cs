using System.Web.Mvc;

namespace Tacmin.Core.Attributes
{
    public class RestoreModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (filterContext.Controller.TempData.ContainsKey("ModelState"))
            {
                filterContext.Controller.ViewData.ModelState.Merge((ModelStateDictionary)filterContext.Controller.TempData["ModelState"]);
            }
        }
    }
}
