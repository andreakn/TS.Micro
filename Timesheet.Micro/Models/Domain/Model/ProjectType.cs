using System.Collections.Generic;
using System.ComponentModel;

namespace Timesheet.Micro.Models.Domain.Model
{
    public enum ProjectType
    {
        [Description("Internprosjekt")]
        InternalProject = 1,

        [Description("Løpende timer")]
        HourlyBilled = 2,

        [Description("Målpris")]
        TargetPrice = 3,
        
        [Description("Fastpris")]
        FixedPrice = 4
    }
}
