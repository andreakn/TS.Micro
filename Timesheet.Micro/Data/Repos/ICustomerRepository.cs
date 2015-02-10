using System.Collections.Generic;
using Timesheet.Micro.Models.Domain.Model;

namespace Timesheet.Micro.Data.Repos
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        //IEnumerable<Address> GetAddresses(Customer customer);
        //IEnumerable<AddressType> GetAddressTypes();
        //IEnumerable<Contact> GetContacts(Customer customer);
        IEnumerable<Customer> GetAllInactive();
    }
}