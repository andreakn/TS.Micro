using System.Data;
using Migrator.Framework;

namespace Timesheet.Micro.Data.Migrations
{
    [Migration(5)]
    public class M005_Projects:BaseMigration
    {

        public override void Up()
        {
            Database.AddTable(projects,
                              new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                              new Column("CustomerId", DbType.Int32, ColumnProperty.Null),
                              new Column("ProjectTypeId", DbType.Int32, ColumnProperty.NotNull),
                              new Column("ProjectCode", DbType.String,50, ColumnProperty.NotNull),
                              new Column("ExternalProjectCode", DbType.String, 50, ColumnProperty.Null),
                              new Column("Name", DbType.String, 50, ColumnProperty.NotNull),
                              new Column("Description", DbType.String, 4000, ColumnProperty.Null),
                              new Column("InvoiceGuidance", DbType.String, 4000, ColumnProperty.Null),
                              new Column("IsBillable", DbType.Boolean, ColumnProperty.NotNull),
                              new Column("IsAvailableToAll", DbType.Boolean, ColumnProperty.NotNull),
                              new Column("EstimateDuration", DbType.Double, ColumnProperty.Null),
                              new Column("CompletionDate", DbType.DateTime, ColumnProperty.Null),
                              new Column("ProjectManagerId", DbType.Int32, ColumnProperty.Null)
                );
            AddCreatedAndModifiedBy(projects);
            AddCreatedAndModifiedDate(projects);
            AddForeignKey(projects,"CustomerId",customers);
            AddForeignKey(projects, "ProjectTypeId", projTypes);
            AddForeignKey(projects, "ProjectManagerId", employees);

            Database.AddTable(projMemb,
                              new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                              new Column("ProjectId", DbType.Int32, ColumnProperty.NotNull),
                              new Column("EmployeeId", DbType.Int32, ColumnProperty.NotNull),
                              new Column("HourlyRate", DbType.Double, ColumnProperty.Null));
            AddForeignKey(projMemb, "ProjectId", projects);
            AddForeignKey(projMemb, "EmployeeId", employees);
        }

        public override void Down()
        {
            Database.RemoveTable(projMemb);
            Database.RemoveTable(projects);
        }
    }
}
