using System;
using System.Web.Mvc;
using Timesheet.Micro.Models.Services;

namespace Timesheet.Micro.Utils
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UserSessionAttribute : ActionFilterAttribute
    {
        private readonly IUserSession _userSession;

        public UserSessionAttribute(IUserSession session)
        {
            _userSession = session;
        }



        //public UserSessionAttribute() : this(ObjectFactory.GetInstance<IUserSession>()){}
        
        //public override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    var controller = filterContext.Controller;
        //    var userInfo = controller.ControllerContext.HttpContext.Session[SessionKeys.SessionUserInfo] as UserInfo ??
        //                   _userSession.GetCurrentUserInfo();
        //    if (userInfo != null)
        //    {
        //        controller.ViewData.Add(userInfo);
        //    }
        //}
    }
}
