using Migrator.Framework;

namespace Timesheet.Micro.Data.Migrations
{
    [Migration(8)]
    public class M008_ActiveBit: BaseMigration
    {
        public override void Up()
        {
            AddActiveColumn(employees);
            AddActiveColumn(roles);
            AddActiveColumn(contacts);
            AddActiveColumn(customers);
            AddActiveColumn(projects);
            AddActiveColumn(projMemb);
            AddActiveColumn(projTypes);
            AddActiveColumn(addressTypes);
        }

        public override void Down()
        {
            RemoveActiveColumn(employees);
            RemoveActiveColumn(roles);
            RemoveActiveColumn(contacts);
            RemoveActiveColumn(customers);
            RemoveActiveColumn(projects);
            RemoveActiveColumn(projMemb);
            RemoveActiveColumn(projTypes);
            RemoveActiveColumn(addressTypes);
        }
    }
}
