using System.Data;
using Migrator.Framework;

namespace Timesheet.Micro.Data.Migrations
{
    [Migration(9)]
    public class M009_StaffableProjectTypes: BaseMigration
    {
        public override void Up()
        {
            Database.AddColumn(projTypes, new Column("IsStaffable", DbType.Boolean, ColumnProperty.NotNull, true));
        }

        public override void Down()
        {
            Database.RemoveColumn(projTypes, "IsStaffable");
        }
    }
}
