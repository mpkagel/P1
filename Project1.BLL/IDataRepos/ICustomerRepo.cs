using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BLL.IDataRepos
{
     public interface ICustomerRepo
    {
        bool CheckCustomerExists(int customerId);
        bool CheckCustomerFullNameExists(string fullName);
        void AddCustomer(Project1.BLL.Customer customer);
        int GetLastCustomerAdded();
        IEnumerable<Project1.BLL.Customer> GetAllCustomers();
        IEnumerable<Project1.BLL.Order> GetCustomerOrderHistory(int customerId);
        IEnumerable<Project1.BLL.OrderItem> GetCustomerOrderItems(int customerId);
    }
}
