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
        private ViewModelAssembler assembler;

        public CustomerController(ICustomerRepository customerRepo, IProjectMemberRepository projectMemberRepository, IProjectRepository projectRepository, IEmployeeRepository employeeRepository, ViewModelAssembler viewModelAssembler)
        {
            this.customerRepo = customerRepo;
            this.projectMemberRepository = projectMemberRepository;
            this.projectRepository = projectRepository;
            this.employeeRepository = employeeRepository;
            this.assembler = viewModelAssembler;
        }

        public ActionResult Index()
        {
            var customers = customerRepo.GetAllActive();
            var projectMembers = projectMemberRepository.GetAllActive();
            var projects = projectRepository.GetAllActive();
            var employees = employeeRepository.GetAllActive();

            var viewmodel = assembler.AssembleCustomerOverviews(customers, projects, projectMembers, employees);

            return View(viewmodel);
        }
        public ActionResult ShowHidden()
        {
            var customers = customerRepo.GetAllInactive();
            var projectMembers = projectMemberRepository.GetAllActive();
            var projects = projectRepository.GetAllActive();
            var employees = employeeRepository.GetAllActive();

            var viewmodel = assembler.AssembleCustomerOverviews(customers, projects, projectMembers, employees);

            return View(viewmodel);
        }

        public ActionResult Create()
        {
            var customer = new Customer();
            customer.IsActive = true;
            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = customerRepo.GetById(id);
            return View(customer);
        }

        [HttpPost]
        public ActionResult Create(Customer customer )
        {
            customerRepo.Save(customer);
            Info("Lagret ny kunde: " + customer.Name);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            customerRepo.Save(customer);
            Info("Oppdaterte kunde: " + customer.Name);
            return RedirectToAction("Index");
        }








    }
}