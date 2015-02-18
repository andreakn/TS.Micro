using System.Collections.Generic;
using Dapper;
using Timesheet.Micro.Models.Domain.Model;

namespace Timesheet.Micro.Data.Repos
{
    public interface IProjectRepository : IRepository<Project>
    {
        IEnumerable<Project> GetCustomerProjects(Customer customer, bool getInactive);
        IEnumerable<Project> GetCurrentlyActiveEmployeeProjects(Employee employee);
        IEnumerable<Project> GetEmployeeProjectsWithRegisteredHoursForWeek(Employee employee, int year, int week);
        IEnumerable<ProjectMember> GetProjectMembers(Project project);
        IEnumerable<Project> GetAllInactive();
    }


    public class ProjectRepository : BaseRepo<Project>, IProjectRepository
    {
        public IEnumerable<Project> GetCustomerProjects(Customer customer, bool getInactive)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Project> GetCurrentlyActiveEmployeeProjects(Employee employee)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Project> GetEmployeeProjectsWithRegisteredHoursForWeek(Employee employee, int year, int week)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ProjectMember> GetProjectMembers(Project project)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Project> GetAllInactive()
        {
          
            using (var conn = GetConn()) { return conn.Query<Project>("select * from Projects where IsActive = 0"); }
        
        }
    }
}