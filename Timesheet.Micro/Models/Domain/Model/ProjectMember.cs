using System.Collections.Generic;

namespace Timesheet.Micro.Models.Domain.Model
{
    public class ProjectMember : PersistentObject
    {
        public virtual int ProjectId { get; set; }
        public virtual int EmployeeId { get; set; }
        public virtual double? HourlyRate { get; set; }
        public virtual bool IsActive { get; set; }
  
        public override bool Equals(object obj)
        {
            var projectMember = obj as ProjectMember;
            if (projectMember != null)
            {
                return ProjectId.Equals(projectMember.ProjectId) &&
                       EmployeeId.Equals(projectMember.EmployeeId);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override IEnumerable<string> FieldsToSave()
        {
            return new[] {"ProjectId", "EmployeeId", "HourlyRate", "IsActive"};
        }
    }
}
