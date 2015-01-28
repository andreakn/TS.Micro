using System.Data;
using Migrator.Framework;

namespace Timesheet.Micro.Data.Migrations
{
    [Migration(11)]
    public class M011_ProjectTimeRate : BaseMigration
    {
        public override void Up()
        {
            Database.AddColumn(projects, new Column("ProjectHourlyRate", DbType.Double, ColumnProperty.Null));
        }

        public override void Down()
        {
            Database.RemoveColumn(projects, "ProjectHourlyRate");
        }
    }
}
