using Timesheet.Micro.Models.Domain.Model;

namespace Timesheet.Micro.Models.Services
{
    public interface IUserSession
    {
        User GetCurrentUser();
        int GetCurrentEmployeeId();
        UserInfo GetCurrentUserInfo();
        UserInfo GetUserInfo(User user);
        void LogIn(User user);
        void LogOut();
    }
}
