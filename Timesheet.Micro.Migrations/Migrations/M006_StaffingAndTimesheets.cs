using System.Data;
using Migrator.Framework;

namespace Timesheet.Micro.Data.Migrations
{
    [Migration(6)]
    public class M006_StaffingAndTimesheets: BaseMigration
    {
        public override void Up()
        {
            Database.AddTable(staffEnt,
                new Column("ProjectId",DbType.Int32, ColumnProperty.PrimaryKey),
                new Column("EmployeeId",DbType.Int32, ColumnProperty.PrimaryKey),
                new Column("Date",DbType.DateTime, ColumnProperty.PrimaryKey),
                new Column("Hours",DbType.Double, ColumnProperty.Null));

            AddForeignKey(staffEnt, "ProjectId", projects);
            AddForeignKey(staffEnt, "EmployeeId", employees);

            Database.AddTable(timeEnt,
                new Column("ProjectId",DbType.Int32, ColumnProperty.PrimaryKey),
                new Column("EmployeeId",DbType.Int32, ColumnProperty.PrimaryKey),
                new Column("Date",DbType.DateTime, ColumnProperty.PrimaryKey),
                new Column("Hours",DbType.Double, ColumnProperty.Null),
                new Column("HourlyRate",DbType.Double, ColumnProperty.Null),
                new Column("Comment", DbType.String, ColumnProperty.Null));

            AddForeignKey(timeEnt, "ProjectId", projects);
            AddForeignKey(timeEnt, "EmployeeId", employees);
        }

        public override void Down()
        {
           Database.RemoveTable(timeEnt);
           Database.RemoveTable(staffEnt);
        }
    }
}
