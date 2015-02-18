using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Timesheet.Micro.Models.Domain.Model;

namespace Timesheet.Micro.Models.Viewmodels
{
    public class ProjectEditModel
    {
        public ProjectEditModel()
        {
            AllactiveEmployees = new List<Employee>();
            ProjectMembers = new List<ProjectMember>();
            Project = new Project();
            Customer = new Customer();
        }

        public int CustomerId { get; set; }

        public string ProjectName
        {
            get
            {
                if (Project != null) return Project.Name;
                return "???";
            }
        }

        public Project Project{ get; set; }
        public Customer Customer{ get; set; }
        public List<Employee> AllactiveEmployees { get; set; }
        public List<ProjectMember> ProjectMembers{ get; set; }

        public Employee GetEmployee(int employeeId)
        {
            return AllactiveEmployees.Single(e => e.Id == employeeId);
        }
    }
}