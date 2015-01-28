using System.Collections.Generic;
using System.Linq;
using Dapper;
using Timesheet.Micro.Models.Domain.Model;

namespace Timesheet.Micro.Data.Repos
{
    public class UserRepository: BaseRepo<User>, IUserRepository
    {
       
        public User GetByUserName(string username)
        {
            using (var conn = GetConn())
            {
                return conn.Query<User>("select * from Users where Username = @username", new {username}).Single();
            }
        }
        
        public bool UsernameExists(string username)
        {
            using (var conn = GetConn())
            {
                return conn.ExecuteScalar<int>("select count(*) from Users where Username = @username", new {username})>0;
            }
        }

    }
}