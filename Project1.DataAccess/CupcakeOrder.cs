using System;
using System.Collections.Generic;

#nullable disable

namespace Project1.DataAccess
{
    public partial class CupcakeOrder
    {
        public CupcakeOrder()
        {
            CupcakeOrderItems = new HashSet<CupcakeOrderItem>();
        }

        public int OrderId { get; set; }
        public int LocationId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderTime { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<CupcakeOrderItem> CupcakeOrderItems { get; set; }
    }
}
