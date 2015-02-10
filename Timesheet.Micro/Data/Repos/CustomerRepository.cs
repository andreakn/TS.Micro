using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using Timesheet.Micro.Models.Domain.Model;

namespace Timesheet.Micro.Data.Repos
{
    public class CustomerRepository: BaseRepo<Customer>, ICustomerRepository
    {
        public IEnumerable<Customer> GetAllInactive()
        {
            using (var conn = GetConn()) { return conn.Query<Customer>("select * from Customers where IsActive = 0"); }
        }

    }
}