using System.Data;
using Migrator.Framework;

namespace Timesheet.Micro.Data.Migrations
{
    [Migration(10)]
    public class M010_PublicHolidays : BaseMigration
    {
        public override void Up()
        {
            Database.AddTable(publicHolidays,
                              new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                              new Column("Date", DbType.DateTime, ColumnProperty.Null),
                              new Column("Description", DbType.String, 255, ColumnProperty.NotNull)
                );
        }

        public override void Down()
        {
            Database.RemoveTable(publicHolidays);
        }
    }
}
    