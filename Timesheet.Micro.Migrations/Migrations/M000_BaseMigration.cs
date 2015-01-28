using System.Data;
using Migrator.Framework;

namespace Timesheet.Micro.Data.Migrations
{
    public abstract class BaseMigration:Migration
    {
        protected string addresses =    "Addresses";
        protected string addressTypes = "AddressTypes";
        protected string contacts =     "Contacts";
        protected string customers =    "Customers";
        protected string empRoles =     "EmployeeRoles";
        protected string employees =    "Employees";
        protected string invoices =     "Invoices";
        protected string projMemb =     "ProjectMembers";
        protected string projects =     "Projects";
        protected string projTypes =    "ProjectTypes";
        protected string roles =        "Roles";
        protected string staffEnt =     "StaffingEntries";
        protected string timeEnt =      "TimeEntries";
        protected string users =        "Users";
        protected string publicHolidays = "PublicHolidays";
        
        protected void AddCreatedAndModifiedBy(string tablename)
        {
            Database.AddColumn(tablename, new Column("CreatedBy", DbType.Int32, ColumnProperty.NotNull));
            Database.AddColumn(tablename, new Column("ModifiedBy", DbType.Int32, ColumnProperty.NotNull));
            AddForeignKey(tablename, "CreatedBy", employees);
            AddForeignKey(tablename, "ModifiedBy", employees);
        }  
        
        protected void AddCreatedAndModifiedDate(string tablename)
        {
            Database.AddColumn(tablename, new Column("CreatedDate", DbType.DateTime, ColumnProperty.NotNull));
            Database.AddColumn(tablename, new Column("ModifiedDate", DbType.DateTime, ColumnProperty.NotNull));
        }
        public void AddForeignKey(string foreignTable, string foreignColumn, string primaryTable)
        {
            string name = GetForeignKeyName(foreignTable, foreignColumn, primaryTable);
            Database.AddForeignKey(name, foreignTable, foreignColumn, primaryTable, "Id");
        }

        protected static string GetForeignKeyName(string foreignTable, string foreignColumn, string primaryTable)
        {
            string name = string.Format("FK_{0}_{1}_{2}", foreignTable, primaryTable, foreignColumn);
            return name;
        }

        protected void AddActiveColumn(string table)
        {
            Database.AddColumn(table, new Column("IsActive", DbType.Boolean, ColumnProperty.NotNull, true));
        }

        protected void RemoveActiveColumn(string table)
        {
            Database.RemoveColumn(table, "IsActive");
        }

    }
}
