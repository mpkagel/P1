using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Logging;
using P1B = Project1.BLL;
using Project1.BLL.IDataRepos;

namespace Project1.DataAccess.DataRepos
{
    public class CustomerRepo : IProject1Repo, ICustomerRepo
    {
        private readonly ILogger<CustomerRepo> _logger;

        private static Project1Context Context { get; set; }

        public CustomerRepo(Project1Context dbContext)
        {
            Context = dbContext;
        }

        public void SaveChangesAndCheckException()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.ToString());
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
            }
        }

        public bool CheckCustomerExists(int customerId)
        {
            try
            {
                return Context.Customers.Any(l => l.CustomerId == customerId);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public bool CheckCustomerFullNameExists(string fullName)
        {
            try
            {
                return Context.Customers.Any(l => (l.FirstName + " " + l.LastName) == fullName);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public void AddCustomer(Project1.BLL.Customer customer)
        {
            var newCustomer = new Project1.DataAccess.Customer
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                DefaultLocation = customer.DefaultLocation
            };
            Context.Customers.Add(newCustomer);
            SaveChangesAndCheckException();
        }

        public int GetLastCustomerAdded()
        {
            try
            {
                return Context.Customers.OrderByDescending(x => x.CustomerId).First().CustomerId;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return -1;
            }
        }

        public IEnumerable<P1B.Customer> GetAllCustomers()
        {
            try
            {
                return Mapper.Map(Context.Customers.ToList());
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public IEnumerable<P1B.Order> GetCustomerOrderHistory(int customerId)
        {
            try
            {
                return Mapper.Map(Context.CupcakeOrders.Where(co => co.CustomerId == customerId).ToList());
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public IEnumerable<P1B.OrderItem> GetCustomerOrderItems(int customerId)
        {
            try
            {
                var customerOrders = Context.CupcakeOrders.Where(co => co.CustomerId == customerId)
                .Select(co => co.OrderId)
                .ToList();
                return Mapper.Map(Context.CupcakeOrderItems.Where(coi => customerOrders.Contains(coi.OrderId)));
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
