﻿using System;
using System.Web.Mvc;

//using Elmah;

namespace Timesheet.Micro.Utils
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ElmahHandleErrorAttribute : HandleErrorAttribute
    {
        //public override void OnException(ExceptionContext filterContext)
        //{
        //    base.OnException(filterContext);

        //    var e = filterContext.Exception;
        //    if (!filterContext.ExceptionHandled   
        //        || RaiseErrorSignal(e)      
        //        || IsFiltered(filterContext))     
        //        return;

        //    LogException(e);
        //}

        //private static bool RaiseErrorSignal(Exception e)
        //{
        //    var context = HttpContext.Current;
        //    if (context == null)
        //        return false;
        //    var signal = ErrorSignal.FromContext(context);
        //    if (signal == null)
        //        return false;
        //    signal.Raise(e, context);
        //    return true;
        //}

        //private static bool IsFiltered(ExceptionContext context)
        //{
        //    var config = context.HttpContext.GetSection("elmah/errorFilter")
        //                 as ErrorFilterConfiguration;

        //    if (config == null)
        //        return false;

        //    var testContext = new ErrorFilterModule.AssertionHelperContext(
        //        context.Exception, HttpContext.Current);

        //    return config.Assertion.Test(testContext);
        //}

        //private static void LogException(Exception e)
        //{
        //    var context = HttpContext.Current;
        //    ErrorLog.GetDefault(context).Log(new Error(e, context));
        //}
    }
}