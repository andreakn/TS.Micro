using System.Data;
using Migrator.Framework;

namespace Timesheet.Micro.Data.Migrations
{
    [Migration(4)]
    public class M004_Customers:BaseMigration
    {
        
        public override void Up()
        {
            Database.AddTable(customers,
                              new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                               new Column("Name", DbType.String, 50, ColumnProperty.NotNull),
                               new Column("Code", DbType.String, 50, ColumnProperty.NotNull),
                               new Column("Phone", DbType.String, 50, ColumnProperty.Null),
                               new Column("Description", DbType.String, 4000, ColumnProperty.Null),
                               new Column("CustomerContactId", DbType.Int32, ColumnProperty.Null)
                );
            AddCreatedAndModifiedDate(customers);
            AddCreatedAndModifiedBy(customers);
            AddForeignKey(customers, "CustomerContactId", employees);
            
            Database.AddTable(contacts,
                             new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                             new Column("CustomerId", DbType.Int32, ColumnProperty.NotNull),
                              new Column("FirstName", DbType.String, 50, ColumnProperty.NotNull),
                              new Column("LastName", DbType.String, 50, ColumnProperty.NotNull),
                              new Column("Phone", DbType.String, 50, ColumnProperty.Null),
                              new Column("Email", DbType.String, 50, ColumnProperty.NotNull),
                              new Column("Role", DbType.String, 50, ColumnProperty.NotNull)
                              );
            
            AddForeignKey(contacts,"CustomerId",customers);


            Database.AddTable(addresses,
                             new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
                             new Column("CustomerId", DbType.Int32, ColumnProperty.NotNull),
                             new Column("AddressTypeId", DbType.Int32, ColumnProperty.NotNull),
                              new Column("AddressText", DbType.String, 255, ColumnProperty.NotNull),
                              new Column("PostalCode", DbType.String, 50, ColumnProperty.NotNull),
                              new Column("City", DbType.String, 50, ColumnProperty.Null)
                              );
            AddForeignKey(addresses, "CustomerId", customers);
            AddForeignKey(addresses, "AddressTypeId", addressTypes);
        }

       

        public override void Down()
        {
            Database.RemoveTable(addresses);
            Database.RemoveTable(contacts);
            Database.RemoveTable(customers);
        }
    }
}
