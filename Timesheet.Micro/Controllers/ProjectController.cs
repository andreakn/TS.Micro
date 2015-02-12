using System.Web.Mvc;
using Timesheet.Micro.Models.Domain.Model;

namespace Timesheet.Micro.Controllers
{
    public class ProjectController:BaseController
    {
        public ActionResult Create(int CustomerId)
        {
            var p = new Project
            {
                CustomerId = CustomerId,
                IsActive = true,
                IsBillable = true,
                ProjectType = ProjectType.HourlyBilled
            };
            return View(p);
        }

        [HttpPost]
        public ActionResult Create(Project project)
        {
            Info("Prosjekt {0} ble opprettet", project.Name);
            return RedirectToAction("Index", "Customer");
        }
    }
}