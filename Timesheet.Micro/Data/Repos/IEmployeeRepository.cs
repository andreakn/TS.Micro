using System.Collections.Generic;
using System.Linq;
using Dapper;
using Timesheet.Micro.Models.Domain.Model;

namespace Timesheet.Micro.Data.Repos
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        IEnumerable<Project> GetCurrentProjects(Employee employee);
        Employee GetByUser(User user);
        IEnumerable<Employee> GetAllInActive();
    }

    public class EmployeeRepository : BaseRepo<Employee>, IEmployeeRepository
    {
        public IEnumerable<Project> GetCurrentProjects(Employee employee)
        {
            using (var conn = GetConn())
            {
                return conn.Query<Project>("select * from Project where IsActive = 1 AND id in (select ProjectId from ProjectMembers where EmployeeId = @Id and IsActive = 1)", new { employee.Id });
            }
        }

        public Employee GetByUser(User user)
        {
            using (var conn = GetConn()){return conn.Query<Employee>("select * from Employees where UserId = @Id", new {user.Id}).Single();}
        }

        public IEnumerable<Employee> GetAllInActive()
        {
            using (var conn = GetConn()){return conn.Query<Employee>("select * from Employees where IsActive = 0");}
        }
    }
}