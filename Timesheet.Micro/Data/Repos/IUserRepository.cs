using Timesheet.Micro.Models.Domain.Model;

namespace Timesheet.Micro.Data.Repos
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByUserName(string username);
        bool UsernameExists(string username);
    }
}