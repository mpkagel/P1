using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1.BLL
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int? DefaultLocation { get; set; }

        public string ReturnFullName()
        {
            return FirstName + " " + LastName;
        }

        public static bool CheckCustomerCanOrder(int customerId, int locationId,
            List<Order> orders)
        {
            return !orders.Where(o => o.OrderCustomer == customerId)
                        .Where(o => o.OrderLocation == locationId)
                        .Any(o => DateTime.Now.Subtract(o.OrderTime).TotalMinutes < 120);
        }
    }
}
