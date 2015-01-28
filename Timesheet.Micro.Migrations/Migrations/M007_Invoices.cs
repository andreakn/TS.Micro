using System.Data;
using Migrator.Framework;

namespace Timesheet.Micro.Data.Migrations
{
    [Migration(7)]
    public class M007_Invoices: BaseMigration
    {
        public override void Up()
        {
            Database.AddTable(invoices,
                new Column("Id",DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                new Column("CustomerId",DbType.Int32, ColumnProperty.NotNull),
                new Column("ProjectId",DbType.Int32, ColumnProperty.Null),
                new Column("ReceiverId",DbType.Int32, ColumnProperty.Null),
                new Column("AddressId",DbType.Int32, ColumnProperty.Null),
                new Column("FromDate",DbType.DateTime, ColumnProperty.NotNull),
                new Column("ToDate",DbType.DateTime, ColumnProperty.NotNull),
                new Column("SentDate",DbType.DateTime, ColumnProperty.Null),
                new Column("DueDate",DbType.DateTime, ColumnProperty.Null),
                new Column("PaidDate",DbType.DateTime, ColumnProperty.Null),
                new Column("Amount",DbType.Double, ColumnProperty.Null),
                new Column("Description",DbType.String, ColumnProperty.Null));
            AddCreatedAndModifiedDate(invoices);
            AddCreatedAndModifiedBy(invoices);
            AddForeignKey(invoices,"AddressId",addresses);
            AddForeignKey(invoices,"CustomerId",customers);
            AddForeignKey(invoices,"ReceiverId",contacts);
            AddForeignKey(invoices,"ProjectId",projects);

            AddForeignKey(projects,"ProjectTypeId",projTypes); //had forgotten this one in the projects migration
        }

        public override void Down()
        {
            Database.RemoveForeignKey(projects, GetForeignKeyName(projects, "ProjectTypeId",projTypes));

            Database.RemoveTable(invoices); 
        }
    }
}
