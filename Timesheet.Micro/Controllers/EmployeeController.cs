using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Timesheet.Micro.Data.Repos;
using Timesheet.Micro.Models.Domain.Model;
using Timesheet.Micro.Models.Extensions;
using Timesheet.Micro.Models.Utils;

namespace Timesheet.Micro.Controllers
{
    public class EmployeeOverview
    {
        public Employee Employee{ get; set; }
        public Dictionary<ProjectMember,Project> ProjectMemberships { get; set; }

        public string GetFilter()
        {
            return (Employee.FullName + " " + string.Join(" ",ProjectMemberships.Select(pm => pm.Value.Name))).ToLowerInvariant();
        }
    }

    public class EmployeeController : BaseController
    {
        private IUserRepository _userRepo;
        private IEmployeeRepository _employeeRepo;
        private IProjectRepository _projectRepo;
        private IProjectMemberRepository _projectMemberRepo;
        private AuthUtil authUtil;

        public EmployeeController(IUserRepository userRepo, IEmployeeRepository employeeRepo, IProjectRepository projectRepo, IProjectMemberRepository projectMemberRepo, AuthUtil authUtil)
        {
            _userRepo = userRepo;
            _employeeRepo = employeeRepo;
            _projectRepo = projectRepo;
            _projectMemberRepo = projectMemberRepo;
            this.authUtil = authUtil;
        }

        // GET: Employee
        public ActionResult Index()
        {
            var employees = _employeeRepo.GetAllActive();
            var list = GetOverviewForEmployees(employees);

            return View(list);
        }
        public ActionResult ShowHidden()
        {
            var employees = _employeeRepo.GetAllInActive();
            var list = GetOverviewForEmployees(employees);

            return View(list);
        }

        private List<EmployeeOverview> GetOverviewForEmployees(IEnumerable<Employee> employees)
        {
            var projects = _projectRepo.GetAllActive();
            var projectMemberships = _projectMemberRepo.GetAllActive();

            var list = new List<EmployeeOverview>();

            foreach (var employee in employees.OrderBy(e=>e.FullName))
            {
                var eo = new EmployeeOverview
                         {
                             Employee = employee,
                             ProjectMemberships = new Dictionary<ProjectMember, Project>()
                         };
                list.Add(eo);

                foreach (var memberships in projectMemberships.Where(pm => pm.EmployeeId == employee.Id))
                {
                    var project = projects.FirstOrDefault(p => p.Id == memberships.ProjectId);
                    if (project == null) continue; //inactive
                    eo.ProjectMemberships[memberships] = project;
                }
            }
            return list;
        }

        

        public ActionResult Create()
        {
            var employee = new Employee();
            employee.IsActive = true;
            employee.Roles = (int) RoleType.User;
            return View(employee);
        }   
        public ActionResult Edit(int id)
        {
            var employee = _employeeRepo.GetById(id);
            return View(employee);
        }
        
        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            _employeeRepo.Save(employee);
            Info("Lagret ansatt "+employee.FullName);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            var username = employee.SuggestUsername();
            if (_userRepo.UsernameExists(username))
            {
                var counter = 1;
                while (_userRepo.UsernameExists(username + counter))
                {
                    counter++;
                }
                username = username + counter;
            }
            _userRepo.Save(authUtil.CreateUser(username, AuthUtil.DEFAULT_PASSWORD));
            Info("Ansatt ble opprettet");
            Info("Brukernavn: " + username);
            Info("Passord: " + AuthUtil.DEFAULT_PASSWORD);

            var user = _userRepo.GetByUserName(username);
            employee.UserId = user.Id;
            _employeeRepo.Save(employee);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult UnlockHours1Month(int id)
        {
            var emp = _employeeRepo.GetById(id);
            var lastLockedHours = emp.LastLockedHours ?? DateTime.Now;

            var newLastLockedHours = lastLockedHours.AddMonths(-1);
            emp.LastLockedHours = newLastLockedHours;
            _employeeRepo.Save(emp);
            Info("Låste timer for {0} tilbakestilt til {1}",emp.FullName,newLastLockedHours.ToFriendly());

            return RedirectToAction("Index");
        }   

    }
}