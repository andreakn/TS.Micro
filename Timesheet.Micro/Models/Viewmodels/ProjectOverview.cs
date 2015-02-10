using System.Collections.Generic;
using System.Linq;
using Timesheet.Micro.Models.Domain.Model;

namespace Timesheet.Micro.Models.Viewmodels
{
    public class ProjectOverview
    {
        public Project Project { get; set; }
        public List<ProjectWorker> Workers { get; set; }

        public ProjectOverview()
        {
            Workers = new List<ProjectWorker>();
        }

        public string GetFilter()
        {
            return (Project.Name +" "+ string.Join(" ", Workers.Select(w => w.GetFilter()))).ToLower();

        }
    }
}