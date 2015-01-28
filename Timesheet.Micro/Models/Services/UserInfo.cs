using System.Collections.Generic;
using Timesheet.Micro.Models.Domain.Model;

namespace Timesheet.Micro.Models.Services
{
    public class UserInfo
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public int EmployeeId { get; set; }
        public RoleType Roles { get; set; }
    }
}
