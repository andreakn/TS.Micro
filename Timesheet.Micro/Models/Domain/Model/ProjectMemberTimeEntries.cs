using System.Collections.Generic;

namespace Timesheet.Micro.Models.Domain.Model
{
    public class ProjectMemberTimeEntries
    {
        public Employee ProjectMember { get; set; }
        public Project Project { get; set; }
        public IEnumerable<TimeEntry> TimeEntries { get; set; }
    }
}
