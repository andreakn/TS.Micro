using System;
using System.Collections.Generic;
using Dapper;
using Timesheet.Micro.Models.Domain.Model;

namespace Timesheet.Micro.Data.Repos
{
    public interface ITimeEntryRepository
    {
        //TimeEntry GetEntry(TimeEntry timeEntry);
        //TimeEntry GetEntry(Project project, Employee employee, DateTime entryDate);
        //void Save(TimeEntry timeEntry);
        //bool Contains(TimeEntry timeEntry);
        //void Delete(TimeEntry timeEntry);
        //IEnumerable<TimeEntry> GetEmployeeEntriesForPeriodAndProject(Employee employee, Project project, DateTime fromDate, DateTime toDate);
        //IEnumerable<TimeEntry> GetEmployeeEntriesForPeriod(Employee employee, DateTime fromDate, DateTime toDate);
        IEnumerable<TimeEntry> GetEmployeeEntriesForPeriod(Employee employee, DateTime fromDate, DateTime toDate);
    }

    public class TimeEntryRepository : BaseRepo<TimeEntry>, ITimeEntryRepository
    {
        public IEnumerable<TimeEntry> GetEmployeeEntriesForPeriod(Employee employee, DateTime fromDate, DateTime toDate)
        {
            using (var conn = GetConn())
            {
                return
                    conn.Query<TimeEntry>(
                        "select * from TimeEntries where employeeId = @employeeId and Date <= @fromDate and Date >= toDate)",
                        new { employeeId = employee.Id });
            }
        }
    }
}