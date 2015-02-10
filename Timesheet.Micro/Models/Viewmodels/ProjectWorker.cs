using Timesheet.Micro.Models.Domain.Model;

namespace Timesheet.Micro.Models.Viewmodels
{
    public class ProjectWorker
    {
        public Employee Employee { get; set; }
        public ProjectMember ProjectMember { get; set; }
        public Project Project { get; set; }

        public double HourlyRate { get { return ProjectMember.HourlyRate ?? Project.ProjectHourlyRate ?? 0; } }
        public string EmployeeName { get { return Employee.FullName; } }
        public string ProjectName { get { return Project.Name; } }
        public bool IsComplete { get { return Project != null && Employee != null && ProjectMember != null; } }

        public string GetFilter()
        {
            return Employee.FullName.ToLower();
        }
    }
}