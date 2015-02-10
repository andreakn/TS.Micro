using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Timesheet.Micro.Data.Repos;
using Timesheet.Micro.Models.Domain.Model;
using Timesheet.Micro.Models.Viewmodels;

namespace Timesheet.Micro.Controllers
{
    public class CustomerController:BaseController
    {
        private ICustomerRepository customerRepo;
        private IProjectMemberRepository projectMemberRepository;
        private IProjectRepository projectRepository;
        private IEmployeeRepository employeeRepository;

        public CustomerController(ICustomerRepository customerRepo, IProjectMemberRepository projectMemberRepository, IProjectRepository projectRepository, IEmployeeRepository employeeRepository)
        {
            this.customerRepo = customerRepo;
            this.projectMemberRepository = projectMemberRepository;
            this.projectRepository = projectRepository;
            this.employeeRepository = employeeRepository;
        }

        public ActionResult Index()
        {
            var customers = customerRepo.GetAllActive();
            var projectMembers = projectMemberRepository.GetAllActive();
            var projects = projectRepository.GetAllActive();
            var employees = employeeRepository.GetAllActive();

            var viewmodel = AssembleCustomerOverviews(customers, projects, projectMembers, employees);

            return View(viewmodel);
        }
        public ActionResult ShowHidden()
        {
            var customers = customerRepo.GetAllInactive();
            var projectMembers = projectMemberRepository.GetAllActive();
            var projects = projectRepository.GetAllActive();
            var employees = employeeRepository.GetAllActive();

            var viewmodel = AssembleCustomerOverviews(customers, projects, projectMembers, employees);

            return View(viewmodel);
        }

        private List<CustomerOverview> AssembleCustomerOverviews(IEnumerable<Customer> customers, IEnumerable<Project> projects, IEnumerable<ProjectMember> projectMembers, IEnumerable<Employee> employees)
        {
            var ret = new List<CustomerOverview>();
            foreach (var customer in customers)
            {
                var customerRet = new CustomerOverview{Customer = customer};
                customerRet.Projects.AddRange(AssembleProjectOverviews(customer,projects,projectMembers,employees));
                ret.Add(customerRet);
            }
            return ret;
        }

        private IEnumerable<ProjectOverview> AssembleProjectOverviews(Customer customer, IEnumerable<Project> projects, IEnumerable<ProjectMember> projectMembers, IEnumerable<Employee> employees)
        {
            var ret = new List<ProjectOverview>();
            var relevantProjects = projects.Where(p => p.CustomerId == customer.Id);
            foreach (var project in relevantProjects)
            {
                var item = new ProjectOverview {Project = project};
                item.Workers.AddRange(AssembleProjectWorkers(project,projectMembers,employees));

                ret.Add(item);
            }

            return ret;
        }

        private IEnumerable<ProjectWorker> AssembleProjectWorkers(Project project, IEnumerable<ProjectMember> projectMembers, IEnumerable<Employee> employees)
        {
            var ret = new List<ProjectWorker>();
            var relevantMembers = projectMembers.Where(m => m.ProjectId == project.Id);
            foreach (var member in relevantMembers)
            {
                var worker = new ProjectWorker {ProjectMember = member, Project = project};
                worker.Employee = employees.FirstOrDefault(e => e.Id == member.EmployeeId);
                ret.Add(worker);
            }

            return ret.Where(w=>w.IsComplete);
        }
    }
}