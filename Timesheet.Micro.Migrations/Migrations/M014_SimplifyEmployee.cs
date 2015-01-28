using System;
using Migrator.Framework;

namespace Timesheet.Micro.Data.Migrations
{
    [Migration(14)]
    public class M014_SimplifyEmployee:BaseMigration
    {
        public override void Up()
        {
            Database.RemoveColumn("Employees","Email");
            Database.RemoveColumn("Employees","Title");
            Database.RemoveColumn("Employees","Phone");
            Database.RemoveColumn("Employees","homePhone");
            Database.RemoveColumn("Employees","BirthDate");
            Database.RemoveColumn("Employees","address");
            Database.RemoveColumn("Employees","City");
            Database.RemoveColumn("Employees","PostalCode");
            Database.RemoveColumn("Employees","CreatedDate");
            Database.RemoveColumn("Employees","ModifiedDate");
            Database.RemoveColumn("Employees","CreatedBy");
            Database.RemoveColumn("Employees","ModifiedBy");
            Database.RemoveTable("EmployeeRoles");
        }

        public override void Down()
        {
            //aint no backing down from this one
            throw new NotSupportedException();
        }
    }
}