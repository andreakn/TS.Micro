namespace Timesheet.Micro.Models.Domain.Model
{
    public class ProjectTimeEntries
    {
        public Project Project { get; set; }
        public TimeEntry[] TimeEntries { get; set; }
    }
}