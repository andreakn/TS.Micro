using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Timesheet.Micro.Models.Extensions;

namespace Timesheet.Micro.Models.Domain.Model
{
    public class Employee : PersistentObject
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual DateTime? LastLockedHours { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }

        public virtual int UserId { get; set; }
        public virtual int Roles { get; set; }
        public virtual bool IsActive { get; set; }
        
        public virtual string FullName
        {
            get
            {
                if (FirstName == null && LastName == null)
                    return string.Empty;
                if (FirstName == null)
                    return LastName;
                if (LastName == null)
                    return FirstName;
                return FirstName + " " + LastName;
            }
        }

        public string LastLockedClass
        {
            get
            {
                var lastDayOfLastMonth = DateTime.Now.FirstDateOfMonth().AddDays(-1);
                if (LastLockedHours >= lastDayOfLastMonth)
                    return "locked-ok";
                return "locked-not-ok";
            }
        }

        public static Employee Dummy()
        {
             return new Employee() {FirstName = "ukjent", LastName = "ukjent"}; 
        }

        public override IEnumerable<string> FieldsToSave()
        {
            return new[] { "FirstName", "LastName", "LastLockedHours", "StartDate", "EndDate", "UserId", "Roles","IsActive" };
        }

        public string SuggestUsername()
        {
            return FirstName + "." + LastName;
        }

        public bool HasRole(RoleType role)
        {
            return (Roles & ((int) role)) == (int)role;
        }
    }

    public static class ListExtension
    {
        public static IEnumerable<SelectListItem> ToSelectListItems(this IEnumerable<Employee> employees, bool includeNoValue=true, Employee selectedEmployee = null)
        {
            var list = employees.Select(e => new SelectListItem
                                         {
                                             Text = e.FullName,
                                             Value = "" + e.Id,
                                             Selected = selectedEmployee != null && selectedEmployee.Id == e.Id
                                         }).ToList();
            if (includeNoValue)
            {
                list.Insert(0,new SelectListItem{Text="-- Velg konsulent --",Value = ""});
            }
            return list;

        } 
    }
}
