using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Timesheet.Micro.Data.Repos;
using Timesheet.Micro.Models;
using Timesheet.Micro.Models.Domain.Model;
using Timesheet.Micro.Models.Extensions;
using Timesheet.Micro.Models.Viewmodels;

namespace Timesheet.Micro.Controllers
{
    public class TimeEntryController:BaseController
    {
        private DateTime currDate = DateTime.Now.Date;

        private IEmployeeRepository _employeeRepository;
        private IProjectRepository _projectRepository;
        private ITimeEntryRepository _timeEntryRepository;

        public TimeEntryController(IEmployeeRepository employeeRepository, IProjectRepository projectRepository, ITimeEntryRepository timeEntryRepository)
        {
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
            _timeEntryRepository = timeEntryRepository;
        }

        public ActionResult RegisterFor(int year, int week)
        {
            currDate = DateTimeHelper.FirstDateOfWeek(year, week);
            return Index();
        }

        public ActionResult Index()
        {
            var viewModel = GetViewModel(currDate);
            return View("RegisterHours",viewModel);
        }

        private Employee GetCurrentEmployee()
        {
            var user = Session[Constants.SESSIONKEY_USER] as User;
            if (user != null)
            {
                return _employeeRepository.GetByUser(user);
            }
            return null;
        }

        private RegisterHoursForm GetViewModel(DateTime CurrentDate)
        {
            var employee = GetCurrentEmployee();
            if (employee != null)
            {
                DateTime fromDate;
                if (CurrentDate.GetWeekForDate() == 53 && CurrentDate.Month == 1)
                {
                    fromDate = DateTimeHelper.FirstDateOfWeek(CurrentDate.Year - 1, CurrentDate.GetWeekForDate());
                }
                else
                {
                    fromDate = DateTimeHelper.FirstDateOfWeek(CurrentDate.Year, CurrentDate.GetWeekForDate());
                }
                var toDate = fromDate.AddDays(6);
                var availableProjects = _projectRepository.GetCurrentlyActiveEmployeeProjects(employee);

                var projectsWithRegisteredHours = _projectRepository.GetEmployeeProjectsWithRegisteredHoursForWeek(employee, fromDate.Year, fromDate.GetWeekForDate());
                var employeeTimeEntries = new List<ProjectTimeEntries>();
                projectsWithRegisteredHours = availableProjects.Union(projectsWithRegisteredHours);

                var entries = _timeEntryRepository.GetEmployeeEntriesForPeriod(employee, fromDate, toDate);

                foreach (var project in projectsWithRegisteredHours)
                {
                    var et = new ProjectTimeEntries();
                    var projectTimeEntries = entries.Where(e=>e.ProjectId == project.Id);
                    et.TimeEntries = new TimeEntry[7];
                    et.Project = project;

                    for (int i = 0; i < 7; i++)
                    {
                        int dayNumber = i;
                        et.TimeEntries[i] =
                            projectTimeEntries.FirstOrDefault(x =>
                                                              x.Date == fromDate.AddDays(dayNumber));
                    }
                    employeeTimeEntries.Add(et);
                }
                var viewModel = new RegisterHoursForm(employee, new List<DateTime?>(), fromDate, fromDate.AddDays(-7), fromDate.AddDays(7), projectsWithRegisteredHours,
                                                               availableProjects, employeeTimeEntries);
                return viewModel;
            }
            return null;
        }


    }
}