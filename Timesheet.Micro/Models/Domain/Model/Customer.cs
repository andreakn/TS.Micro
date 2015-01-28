using System.Collections.Generic;

namespace Timesheet.Micro.Models.Domain.Model
{
    public class Customer : PersistentObject
    {
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Description { get; set; }

        public static Customer Dummy()
        {
                return new Customer
                    {
                        Code = "ukjent",
                        Description = "ukjent",
                        Name = "ukjent"
                    };
        }

        public override IEnumerable<string> FieldsToSave()
        {
            return new[] {"Name", "Code", "Phone", "Description"};
        }
    }
}
