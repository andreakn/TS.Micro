using System.Web.Mvc;
using Timesheet.Micro.Data.Repos;

namespace Timesheet.Micro.Controllers
{
    public class HomeController : Controller
    {

        private UserRepository userRepo;

        public HomeController(UserRepository userRepo)
        {
            this.userRepo = userRepo;
        }

        public ActionResult Index()
        {
            return View("Index");
        }


    }
}