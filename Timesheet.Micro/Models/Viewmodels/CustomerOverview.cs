using System.Collections.Generic;
using System.Linq;
using Timesheet.Micro.Models.Domain.Model;

namespace Timesheet.Micro.Models.Viewmodels
{
    public class CustomerOverview
    {
        public Customer Customer { get; set; }
        public List<ProjectOverview> Projects { get; set; }

        public CustomerOverview()
        {
            Projects = new List<ProjectOverview>();
        }

        public string GetFilter()
        {
            return (Customer.Name +" "+ string.Join(" ", Projects.Select(p => p.GetFilter()))).ToLower();
        }
    }
}