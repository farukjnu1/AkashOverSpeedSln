using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AkashOverSpeed.Filters
{
    public class MyFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            #region authentication by session
            if (filterContext.HttpContext.Session["VmUser"] == null)
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
            }
            #endregion
        }
    }
}