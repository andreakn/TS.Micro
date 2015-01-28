using System.Data;
using Migrator.Framework;

namespace Timesheet.Micro.Data.Migrations
{
    [Migration(3)]
    public class M003_Employees : BaseMigration
    {


        public override void Up()
        {
            Database.AddTable(employees,
             new Column("Id", DbType.Int32, ColumnProperty.PrimaryKeyWithIdentity),
             new Column("UserId", DbType.Int32, ColumnProperty.Null),
             new Column("FirstName", DbType.String, 50, ColumnProperty.NotNull),
             new Column("LastName", DbType.String, 50, ColumnProperty.NotNull),
             new Column("Title", DbType.String, 50, ColumnProperty.Null),
             new Column("Email", DbType.String, 50, ColumnProperty.NotNull),
             new Column("Phone", DbType.String, 50, ColumnProperty.Null),
             new Column("HomePhone", DbType.String, 50, ColumnProperty.Null),
             new Column("BirthDate", DbType.String, 50, ColumnProperty.Null),
             new Column("LastLockedHours", DbType.DateTime, ColumnProperty.Null),
             new Column("StartDate", DbType.DateTime, ColumnProperty.Null),
             new Column("EndDate", DbType.DateTime, ColumnProperty.Null),
             new Column("Address", DbType.String, 50, ColumnProperty.Null),
             new Column("City", DbType.String, 50, ColumnProperty.Null),
             new Column("PostalCode", DbType.String, 50, ColumnProperty.Null));
            AddCreatedAndModifiedDate(employees);
            Database.AddColumn(employees, "CreatedBy", DbType.Int32, ColumnProperty.Null);
            Database.AddColumn(employees, "ModifiedBy", DbType.Int32, ColumnProperty.Null);

            AddForeignKey(employees, "UserId", users);
            AddForeignKey(employees, "CreatedBy", employees);  //cannot use base class implementation here as
            AddForeignKey(employees, "ModifiedBy", employees); //employees can be created out of thin air :)


            Database.AddTable(empRoles,
                new Column("EmployeeId", DbType.Int32, ColumnProperty.Null),
                new Column("RoleId", DbType.Int32, ColumnProperty.Null)
                );

            AddForeignKey(empRoles, "EmployeeId", employees);
            AddForeignKey(empRoles, "RoleId", roles);
            InsertStaticData();
        }

        private void InsertStaticData()
        {
            Database.BeginTransaction();
            var userId = Database.ExecuteScalar(string.Format("select id from [{0}] where [Username] = 'admin'", users));

            Database.ExecuteNonQuery(
                string.Format("INSERT INTO [{0}] ([UserId],[FirstName],[LastName],[Email],[CreatedDate],[ModifiedDate]) VALUES ({1} ,'Test','Bruker', 'test@forse.no',getdate(),getdate())", employees, userId));

            var empId = Database.ExecuteScalar(string.Format("select id from [{0}] where [UserId] = {1}", employees, userId));

            Database.ExecuteNonQuery(string.Format("INSERT INTO [{0}] ([RoleId],[EmployeeId]) VALUES (128,{1})", empRoles, empId));
            Database.Commit();
        }

        public override void Down()
        {
            Database.RemoveTable(empRoles);
            Database.RemoveTable(employees);
        }
    }
}
