using System.Collections.Generic;
using System.Linq;

namespace Timesheet.Micro.Models.Domain.Model
{
    public class TimeEntry : ProjectTime
    {
        public virtual double? HourlyRate { get; set; }
        public virtual string Comment { get; set; }
        public override IEnumerable<string> FieldsToSave()
        {
            return base.FieldsToSave().Union(new[] {"HourlyRate", "Comment"});
        }
    }
}
