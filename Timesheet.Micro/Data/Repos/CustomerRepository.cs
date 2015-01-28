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
       
    }
}