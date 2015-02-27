using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Timesheet.Micro.Models.Domain.Model;

namespace Timesheet.Micro.Models.Viewmodels
{
    public class RegisterHoursForm
    {
        public Employee Employee { get; private set; }
        public IEnumerable<DateTime?> LockDates { get; private set; }
        public DateTime Date { get; private set; }
        public DateTime PreviousDate { get; private set; }
        public DateTime NextDate { get; private set; }
        public IEnumerable<Project> AvailableProjects { get; private set; }
        public IEnumerable<Project> Projects { get; private set; }
        public int ProjectCount { get { return Projects != null ? Projects.Count() : 0; } }
        public IEnumerable<ProjectTimeEntries> ProjectTimeEntries { get; private set; }

        public RegisterHoursForm(Employee employee, IEnumerable<DateTime?> lockDates, DateTime date, DateTime prevDate, DateTime nextDate,
                                          IEnumerable<Project> activeProjectsAndProjectsWithEntriesForPeriod, IEnumerable<Project> availableProjects,
                                          IEnumerable<ProjectTimeEntries> projectTimeEntries
            )
        {
            Employee = employee;
            LockDates = lockDates;
            Date = date;
            PreviousDate = prevDate;
            NextDate = nextDate;
            Projects = activeProjectsAndProjectsWithEntriesForPeriod;
            AvailableProjects = availableProjects;
            ProjectTimeEntries = projectTimeEntries;
        }

        public double? GetProjectHours(IEnumerable<ProjectTimeEntries> projectTimeEntries, int projectNum, int timeEntryNum)
        {
            if (projectTimeEntries != null && projectTimeEntries.Count() > projectNum
                && projectTimeEntries.ElementAt(projectNum) != null && projectTimeEntries.ElementAt(projectNum).TimeEntries.Count() > timeEntryNum
                && projectTimeEntries.ElementAt(projectNum).TimeEntries[timeEntryNum] != null)
            {
                return projectTimeEntries.ElementAt(projectNum).TimeEntries[timeEntryNum].Hours;
            }
            return null;
        }

        public string GetProjectHoursComment(IEnumerable<ProjectTimeEntries> projectTimeEntries, int projectNum, int timeEntryNum)
        {
            if (projectTimeEntries != null && projectTimeEntries.Count() > projectNum
                && projectTimeEntries.ElementAt(projectNum) != null && projectTimeEntries.ElementAt(projectNum).TimeEntries.Count() > timeEntryNum
                && projectTimeEntries.ElementAt(projectNum).TimeEntries[timeEntryNum] != null)
            {
                return projectTimeEntries.ElementAt(projectNum).TimeEntries[timeEntryNum].Comment;
            }
            return null;
        }

        public string GetBillableProjects(IEnumerable<Project> projects)
        {
            var billableProjects = from project in projects
                                   where project.IsBillable
                                   select new { Id = project.Id };
            return HttpUtility.HtmlEncode(JsonConvert.SerializeObject(billableProjects));
        }

        public string GetProjectHoursTotal(IEnumerable<ProjectTimeEntries> projectTimeEntries, int projectNum)
        {
            if (projectTimeEntries != null && projectTimeEntries.Count() > projectNum
                && projectTimeEntries.ElementAt(projectNum) != null && projectTimeEntries.ElementAt(projectNum).TimeEntries.Any()
                )
            {
                double? returnValue = projectTimeEntries.ElementAt(projectNum).TimeEntries.Where(x => x != null).Sum(x => x.Hours);
                return returnValue.Value.ToString("0.0", CultureInfo.CurrentCulture);
            }
            return 0.ToString("0.0", CultureInfo.CurrentCulture);
        }

        public bool IsPeriodLocked { get { return GetDayLockStatus(Employee, NextDate.AddDays(-1)); } }

        public bool GetDayLockStatus(Employee employee, DateTime currentDate)
        {
            if (employee != null && employee.LastLockedHours.HasValue)
            {
                return employee.LastLockedHours.Value.Date >= currentDate.Date;
            }
            return false;
        }

        public string GetDayTotalHours(IEnumerable<ProjectTimeEntries> projectTimeEntries, int timeEntryNum)
        {
            double sum = 0;
            if (projectTimeEntries != null && projectTimeEntries.Any())
            {
                foreach (var entry in projectTimeEntries)
                {
                    if (entry.TimeEntries != null && entry.TimeEntries.Any()
                        && entry.TimeEntries[timeEntryNum] != null
                        && entry.TimeEntries[timeEntryNum].Hours.HasValue)
                    {
                        sum += entry.TimeEntries[timeEntryNum].Hours.Value;
                    }
                }
            }
            return sum.ToString("0.0", CultureInfo.CurrentCulture);
        }

        public string GetTotalHours(IEnumerable<ProjectTimeEntries> projectTimeEntries)
        {
            double sum = 0;
            if (projectTimeEntries != null && projectTimeEntries.Any())
            {
                foreach (var entry in projectTimeEntries)
                {
                    if (entry.TimeEntries != null && entry.TimeEntries.Any())
                    {
                        foreach (var timeEntry in entry.TimeEntries)
                        {
                            if (timeEntry != null && timeEntry.Hours.HasValue)
                            {
                                sum += timeEntry.Hours.Value;
                            }
                        }
                    }
                }
            }
            return sum.ToString("0.0", CultureInfo.CurrentCulture);
        }

        public string GetTotalBillableHours(IEnumerable<ProjectTimeEntries> projectTimeEntries)
        {
            double sum = 0;
            if (projectTimeEntries != null && projectTimeEntries.Any())
            {
                foreach (var entry in projectTimeEntries)
                {
                    if (entry.TimeEntries != null && entry.TimeEntries.Any() && entry.Project.IsBillable)
                    {
                        foreach (var timeEntry in entry.TimeEntries)
                        {
                            if (timeEntry != null && timeEntry.Hours.HasValue)
                            {
                                sum += timeEntry.Hours.Value;
                            }
                        }
                    }
                }
            }
            return sum.ToString("0.0", CultureInfo.CurrentCulture);
        }
    }
}