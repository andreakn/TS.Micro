using System;
using System.Collections.Generic;
using Dapper;
using Glimpse.AspNet.Tab;
using Timesheet.Micro.Models.Domain.Model;
using Timesheet.Micro.Models.Extensions;

namespace Timesheet.Micro.Data.Repos
{
    public interface IProjectRepository : IRepository<Project>
    {
        IEnumerable<Project> GetEmployeeProjectsWithRegisteredHoursForWeek(Employee employee, int year, int week);
        IEnumerable<Project> GetAllInactive();
        IEnumerable<Project> GetCurrentlyActiveEmployeeProjects(Employee employee);
    }


    public class ProjectRepository : BaseRepo<Project>, IProjectRepository
    {

        public IEnumerable<Project> GetEmployeeProjectsWithRegisteredHoursForWeek(Employee employee, int year, int week)
        {
            DateTime fromDate = DateTimeHelper.FirstDateOfWeek(year, week);
            DateTime toDate = fromDate.AddDays(6);
            using (var conn = GetConn())
            {
                return
                    conn.Query<Project>(
                        "select * from Projects where Id in (select projectid from TimeEntries where Date >= @fromDate and Date <= ToDate and employeeId = @employeeId)",
                        new {fromDate, toDate, employeeId = employee.Id});
            }
        }

        public IEnumerable<Project> GetAllInactive()
        {
          
            using (var conn = GetConn()) { return conn.Query<Project>("select * from Projects where IsActive = 0"); }
        
        }

        public IEnumerable<Project> GetCurrentlyActiveEmployeeProjects(Employee employee)
        {
            using (var conn = GetConn())
            {
                return
                    conn.Query<Project>(
                        "select * from Projects where IsActive = 1 and ( Id in (select projectid from ProjectMembers where employeeId = @employeeId and IsActive = 1) or IsAvailableToAll)",
                        new { employeeId = employee.Id });
            }
        }
    }
}