using Microsoft.Extensions.Logging;
using Project1.BLL;
using Project1.BLL.IDataRepos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Project1.DataAccess.DataRepos
{
    public class OrderRepo : IProject1Repo, IOrderRepo
    {
        private readonly ILogger<OrderRepo> _logger;

        private static Project1Context Context { get; set; }

        public OrderRepo(Project1Context dbContext)
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

        public bool CheckOrderExists(int orderId)
        {
            try
            {
                return Context.CupcakeOrders.Any(l => l.OrderId == orderId);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public void AddCupcakeOrder(Project1.BLL.Order order)
        {
            var newOrder = new Project1.DataAccess.CupcakeOrder
            {
                LocationId = order.OrderLocation,
                CustomerId = order.OrderCustomer,
                OrderTime = order.OrderTime
            };
            Context.CupcakeOrders.Add(newOrder);
            SaveChangesAndCheckException();
        }

        public void AddCupcakeOrderItems(int orderId, Dictionary<int, int> cupcakeInputs)
        {
            foreach (var cupcake in cupcakeInputs)
            {
                var newOrderItem = new Project1.DataAccess.CupcakeOrderItem
                {
                    OrderId = orderId,
                    CupcakeId = cupcake.Key,
                    Quantity = cupcake.Value
                };
                Context.CupcakeOrderItems.Add(newOrderItem);
            }
            SaveChangesAndCheckException();
        }

        public int GetLastCupcakeOrderAdded()
        {
            try
            {
                return Context.CupcakeOrders.OrderByDescending(x => x.OrderId).First().OrderId;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return -1;
            }
        }

        public Order GetOrder(int orderId)
        {
            try
            {
                return Mapper.Map(Context.CupcakeOrders.Single(co => co.OrderId == orderId));
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public IEnumerable<Order> GetAllOrders()
        {
            try
            {
                return Mapper.Map(Context.CupcakeOrders.ToList());
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public IEnumerable<OrderItem> GetAllOrderItems()
        {
            try
            {
                return Mapper.Map(Context.CupcakeOrderItems.ToList());
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public IEnumerable<OrderItem> GetOrderItems(int orderId)
        {
            try
            {
                return Mapper.Map(Context.CupcakeOrderItems.Where(coi => coi.OrderId == orderId));
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
