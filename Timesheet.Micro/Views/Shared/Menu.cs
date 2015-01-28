using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timesheet.Micro.Views.Shared
{
    public class MenuHelper
    {
        public static string ActiveIfCurrent(string prefix)
        {
            var url = HttpContext.Current.Request.Path;
            if (url.StartsWith("/"))
                url = url.Substring(1);
            if (url.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase))
                return "active";
            return "";
        }
    }
}