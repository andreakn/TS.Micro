using System.Collections.Generic;

namespace Timesheet.Micro.Models.Domain.Model
{
    public class Customer : PersistentObject
    {
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Description { get; set; }
        public virtual bool IsActive { get; set; }

        public static Customer Dummy()
        {
                return new Customer
                    {
                        IsActive = true,
                        Code = "ukjent",
                        Description = "ukjent",
                        Name = "ukjent"
                    };
        }

        public override IEnumerable<string> FieldsToSave()
        {
            return new[] {"Name", "Description","IsActive"};
        }
    }
}
