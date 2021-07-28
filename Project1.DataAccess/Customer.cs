using System;
using System.Collections.Generic;

#nullable disable

namespace Project1.DataAccess
{
    public partial class Customer
    {
        public Customer()
        {
            CupcakeOrders = new HashSet<CupcakeOrder>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? DefaultLocation { get; set; }

        public virtual Location DefaultLocationNavigation { get; set; }
        public virtual ICollection<CupcakeOrder> CupcakeOrders { get; set; }
    }
}
