using System;
using System.Collections.Generic;

#nullable disable

namespace Project1.DataAccess
{
    public partial class Location
    {
        public Location()
        {
            CupcakeOrders = new HashSet<CupcakeOrder>();
            Customers = new HashSet<Customer>();
            LocationInventories = new HashSet<LocationInventory>();
        }

        public int LocationId { get; set; }

        public string Name { get; set; }

        public virtual ICollection<CupcakeOrder> CupcakeOrders { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<LocationInventory> LocationInventories { get; set; }
    }
}
