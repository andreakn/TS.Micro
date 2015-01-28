using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Timesheet.Micro.Data.Repos;
using Timesheet.Micro.Models.Domain.Model;
using Timesheet.Micro.Models.Services;
using Timesheet.Micro.Models.Utils;
using WebGrease.Css.Extensions;

namespace Timesheet.Micro.Controllers
{
    public class UserController : BaseController
    {

        private IUserRepository _userRepository;
        private AuthUtil authUtil;

        public UserController(IUserRepository userRepository, AuthUtil authUtil)
        {
            _userRepository = userRepository;
            this.authUtil = authUtil;
        }

        // GET: User
        public ActionResult Index()
        {   
            var users = _userRepository.GetAll();
           
            return View(users);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string username, string password, string confirmpassword)
        {
            if (password != confirmpassword){Error("Passordene er ikke like");}
            if (_userRepository.UsernameExists(username)){Error("Brukernavnet er tatt, velg et annet");}
            if (password.Length<7)Error("Skjerpings, minst 7 bokstaver i passord");
            if (ErrorMessages.Any())
                return View();

            var user = authUtil.CreateUser(username,password);
            _userRepository.Save(user);
            Info("Bruker opprettet");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SetPassword(string username)
        {
            if (!_userRepository.UsernameExists(username))
            {
                Error("Ukjent bruker");
                return RedirectToAction("Index");
            }
            var user = _userRepository.GetByUserName(username);
            return View(user);
        } 
        
        [HttpPost]
        public ActionResult SetPassword(string username, string newPassword)
        {
            if (!_userRepository.UsernameExists(username))
            {
                Error("Ukjent bruker");
                return RedirectToAction("Index");
            }
            var user = _userRepository.GetByUserName(username);
            authUtil.SetPassword(user, newPassword);
            _userRepository.Save(user);
            Info("Passord lagret");
      
            return RedirectToAction("Index");
        }
    }
}