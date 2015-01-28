//using StructureMap;
using System;
using System.Web.Mvc;

//using Timesheet.Web.Process.Constants;
using Timesheet.Micro.Models.Domain.Model;
using Timesheet.Micro.Models.Services;

namespace Timesheet.Micro.Utils
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        public new RoleType Roles;

        private readonly IUserSession _userSession;

        public AuthorizationAttribute(IUserSession session)
        {
            _userSession = session;
        }

        //public AuthorizationAttribute() : this(ObjectFactory.GetInstance<IUserSession>()) { }
        
        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    if (httpContext == null)
        //        throw new ArgumentNullException("httpContext");
            
        //    if (!httpContext.User.Identity.IsAuthenticated)
        //        return false;

        //    var userInfo = GetUserInfo(httpContext);
            
        //    if (userInfo != null && userInfo.Roles != null)
        //    {
        //        foreach (var role in userInfo.Roles)
        //        {
        //            if ((Roles & role.RoleType) == role.RoleType)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

        //private UserInfo GetUserInfo(HttpContextBase httpContext)
        //{
        //    return httpContext.Session[SessionKeys.SessionUserInfo] as UserInfo ??
        //                   _userSession.GetCurrentUserInfo();
        //}
    }
}
