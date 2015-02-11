using System;
using Migrator.Framework;

namespace Timesheet.Micro.Data.Migrations
{
    [Migration(15)]
    public class M015_SimplifyCustomer:BaseMigration
    {
        public override void Up()
        {
            Database.RemoveColumn("Customers","Code");
            Database.RemoveColumn("Customers", "Phone");
            Database.RemoveColumn("Customers", "CustomerContactId");
            Database.RemoveColumn("Customers", "CreatedDate");
            Database.RemoveColumn("Customers", "ModifiedDate");
            Database.RemoveColumn("Customers", "CreatedBy");
            Database.RemoveColumn("Customers", "ModifiedBy");
        }

        public override void Down()
        {
            //aint no backing down from this one
            throw new NotSupportedException();
        }
    }
}