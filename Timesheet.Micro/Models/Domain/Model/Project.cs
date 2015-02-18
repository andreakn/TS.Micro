using System;
using System.Collections.Generic;

namespace Timesheet.Micro.Models.Domain.Model
{
    public class Project : PersistentObject
    {
        public virtual int CustomerId { get; set; }
        public virtual ProjectType ProjectType { get; set; }
        public virtual string ExternalProjectCode { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual string InvoiceGuidance { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsBillable { get; set; }
        public virtual bool IsAvailableToAll { get; set; }
        public virtual double? EstimateDuration { get; set; }
        public virtual DateTime? CompletionDate { get; set; }
        public virtual double? ProjectHourlyRate { get; set; }
        public override IEnumerable<string> FieldsToSave()
        {
            return new[] { "CustomerId", "ProjectType", "Name", "Description", "InvoiceGuidance", "IsActive", 
                "IsBillable","IsAvailableToAll","ProjectHourlyRate" };
        }

        public string SelectedIfTypeMatch(ProjectType someType)
        {
            if (someType == ProjectType) return "selected";
            return "";
        }

        public string GetToggleActiveStateText()
        {
            if (IsActive) return "inaktiv";
            return "aktiv";
        }
    }
}
