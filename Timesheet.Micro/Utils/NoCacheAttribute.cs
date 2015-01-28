using System;
using System.Web;
using System.Web.Mvc;

namespace Timesheet.Micro.Utils
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        }
    }
}