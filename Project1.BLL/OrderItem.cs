using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BLL
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int CupcakeId { get; set; }
        public int? Quantity { get; set; }
    }
}
