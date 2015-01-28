using System.Data;
using Migrator.Framework;

namespace Timesheet.Micro.Data.Migrations
{
    [Migration(13)]
    public class M013_EmployeeRoles_Simpler:BaseMigration
    {
        public override void Up()
        {
            Database.AddColumn("Employees","Roles",DbType.Int32);
            Database.ExecuteNonQuery(
                "UPDATE T1 SET T1.Roles = (SELECT SUM(distinct T2.RoleId) FROM EmployeeRoles T2 WHERE T2.EmployeeId = T1.ID) FROM Employees T1");
        } 
        public override void Down()
        {
            Database.RemoveColumn("Employees", "Roles");
        }
    }
}