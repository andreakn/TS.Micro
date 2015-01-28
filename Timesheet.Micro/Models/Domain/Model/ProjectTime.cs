using System;
using System.Collections.Generic;
using Timesheet.Micro.Models.Extensions;

namespace Timesheet.Micro.Models.Domain.Model
{
    public abstract class ProjectTime : PersistentObject
    {
        public virtual int ProjectId { get; set; }
        public virtual int EmployeeId { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual double? Hours { get; set; }
        
        public override bool Equals(object obj)
        {
            var timeEntry = obj as ProjectTime;
            if (timeEntry != null )
            {
                return ProjectId.Equals(timeEntry.ProjectId) &&
                       EmployeeId.Equals(timeEntry.EmployeeId) &&
                       Date.CompareDate(timeEntry.Date);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override IEnumerable<string> FieldsToSave()
        {
            return new[] {"ProjectId","EmployeeId","Date","Hours"};
        }
    }
}
