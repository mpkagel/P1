using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using P1B = Project1.BLL;

namespace Project1.Test
{
    public class CustomerTest
    {
        [Fact]
        public void TestCheckCustomerCanOrderTrue()
        {
            // Arrange
            int locationId = 1;
            int customerId = 1;

            List<P1B.Order> orders = new List<P1B.Order>
            {
                new P1B.Order
                {
                    Id = 1,
                    OrderLocation = 1,
                    OrderCustomer = 1,
                    OrderTime = DateTime.Now.AddMinutes(-121)
                }
            };

            // Act and Assert
            Assert.True(P1B.Customer.CheckCustomerCanOrder(customerId, locationId, orders));
        }

        [Fact]
        public void TestCheckCustomerCanOrderFalse()
        {
            // Arrange
            int locationId = 1;
            int customerId = 1;

            List<P1B.Order> orders = new List<P1B.Order>
            {
                new P1B.Order
                {
                    Id = 1,
                    OrderLocation = 1,
                    OrderCustomer = 1,
                    OrderTime = DateTime.Now
                }
            };

            // Act and Assert
            Assert.False(P1B.Customer.CheckCustomerCanOrder(customerId, locationId, orders));
        }
    }
}
