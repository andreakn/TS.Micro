using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timesheet.Micro.Models.Domain.Model;

namespace Timesheet.Micro.Models.Viewmodels
{
    public class ViewModelAssembler
    {


        public List<CustomerOverview> AssembleCustomerOverviews(IEnumerable<Customer> customers, IEnumerable<Project> projects, IEnumerable<ProjectMember> projectMembers, IEnumerable<Employee> employees)
        {
            var ret = new List<CustomerOverview>();
            foreach (var customer in customers)
            {
                var customerRet = new CustomerOverview { Customer = customer };
                customerRet.Projects.AddRange(AssembleProjectOverviews(customer, projects, projectMembers, employees));
                ret.Add(customerRet);
            }
            return ret;
        }

        public IEnumerable<ProjectOverview> AssembleProjectOverviews(Customer customer, IEnumerable<Project> projects, IEnumerable<ProjectMember> projectMembers, IEnumerable<Employee> employees)
        {
            var ret = new List<ProjectOverview>();
            var relevantProjects = projects.Where(p => p.CustomerId == customer.Id);
            foreach (var project in relevantProjects)
            {
                var item = new ProjectOverview { Project = project };
                item.Workers.AddRange(AssembleProjectWorkers(project, projectMembers, employees));

                ret.Add(item);
            }

            return ret;
        }

        public IEnumerable<ProjectWorker> AssembleProjectWorkers(Project project, IEnumerable<ProjectMember> projectMembers, IEnumerable<Employee> employees)
        {
            var ret = new List<ProjectWorker>();
            var relevantMembers = projectMembers.Where(m => m.ProjectId == project.Id);
            foreach (var member in relevantMembers)
            {
                var worker = new ProjectWorker { ProjectMember = member, Project = project };
                worker.Employee = employees.FirstOrDefault(e => e.Id == member.EmployeeId);
                ret.Add(worker);
            }

            return ret.Where(w => w.IsComplete);
        }
    }
}