using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Timesheet.Micro.Controllers
{
    public class BaseController:Controller
    {
        protected List<string> ErrorMessages = new List<string>(); 

        protected void Info(string message)
        {
            AppendTempDataMessage(message, "Info");
        }

        protected void Error(string message)
        {
            ErrorMessages.Add(message);
            AppendTempDataMessage(message, "Error");
        }

        private void AppendTempDataMessage(string message, string severity)
        {
            string oldMessage = "" + TempData[severity];
            if (!string.IsNullOrWhiteSpace(oldMessage))
            {
                oldMessage += "<br/>";
            }
            TempData[severity] = oldMessage + message;
        }
    }
}