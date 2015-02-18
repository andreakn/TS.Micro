using System;
using System.Linq;
using System.Web.Mvc;
using Timesheet.Micro.Data.Repos;
using Timesheet.Micro.Models.Domain.Model;
using Timesheet.Micro.Models.Viewmodels;

namespace Timesheet.Micro.Controllers
{
    public class ProjectController:BaseController
    {

        private IProjectRepository projectRepo;
        private IProjectMemberRepository projectMemberRepo;
        private IEmployeeRepository employeeRepo;
        private ICustomerRepository customerRepo;
        private ViewModelAssembler assembler;

        public ProjectController(IEmployeeRepository employeeRepo, IProjectMemberRepository projectMemberRepo, IProjectRepository projectRepo, ICustomerRepository customerRepo, ViewModelAssembler assembler)
        {
            this.employeeRepo = employeeRepo;
            this.projectMemberRepo = projectMemberRepo;
            this.projectRepo = projectRepo;
            this.customerRepo = customerRepo;
            this.assembler = assembler;
        }

        public ActionResult Index()
        {
            var customers = customerRepo.GetAllActive();
            var projectMembers = projectMemberRepo.GetAllActive();
            var projects = projectRepo.GetAllActive();
            var employees = employeeRepo.GetAllActive();

            var viewmodel = assembler.AssembleCustomerOverviews(customers, projects, projectMembers, employees);
            return View(viewmodel);
        }
        public ActionResult ShowHidden()
        {
            var customers = customerRepo.GetAll();
            var projectMembers = projectMemberRepo.GetAllActive();
            var projects = projectRepo.GetAllInactive();
            var employees = employeeRepo.GetAllActive();

            var viewmodel = assembler.AssembleCustomerOverviews(customers, projects, projectMembers, employees);

            return View(viewmodel);
        }

        public ActionResult Create(int customerId)
        {
            var pe = new ProjectEditModel
                     {
                         Project = new Project
                                   {
                                       CustomerId = customerId,
                                       IsActive = true,
                                       IsBillable = true,
                                       ProjectType= ProjectType.HourlyBilled
                                   },
                         AllactiveEmployees = employeeRepo.GetAllActive().ToList(),
                         Customer = customerRepo.GetById(customerId),
                     };
            return View(pe);
        }

        [HttpPost]
        public ActionResult Create(ProjectEditModel model)
        {
          
            SaveEditModel(model);
            Info("Prosjekt {0} ble Lagret", model.ProjectName);

            return RedirectToAction("Index", "Customer");
        }

        private void SaveEditModel(ProjectEditModel model)
        {
            var project = model.Project;
            projectRepo.Save(project);
            var oldProjectMembers = projectMemberRepo.GetForProject(project.Id);
            foreach (var oldMember in oldProjectMembers)
            {
                if (model.ProjectMembers.All(p => p.Id != oldMember.Id))
                {
                    projectMemberRepo.Delete(oldMember);
                }
            }
            foreach (var newMember in model.ProjectMembers.Where(pm=>pm.EmployeeId>0))
            {
                projectMemberRepo.Save(newMember);
            }
        }

        public ActionResult Edit(int id)
        {
            var projectId = id;
            var project = projectRepo.GetById(projectId);
            var pe = new ProjectEditModel
                     {
                         Project = project,
                         AllactiveEmployees = employeeRepo.GetAllActive().ToList(),
                         Customer = customerRepo.GetById(project.CustomerId),
                         ProjectMembers = projectMemberRepo.GetForProject(projectId).ToList()
                     };
            return View(pe);
        }

        [HttpPost]
        public ActionResult Edit(ProjectEditModel model)
        {
            SaveEditModel(model);
            Info("Prosjekt {0} ble Lagret", model.Project.Name);

            return RedirectToAction("Index", "Customer");
        }       
        [HttpPost]
        public ActionResult SetIsActive(int id, bool newValue)
        {
            var proj = projectRepo.GetById(id);
            var newState = proj.GetToggleActiveStateText();
            proj.IsActive = newValue;
            projectRepo.Save(proj);
            Info("Prosjekt {0} ble satt til {1}", proj.Name, newState);

            return RedirectToAction("Index", "Project");
        }
    }
}