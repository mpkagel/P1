using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1.BLL
{
    public class Order
    {
        public int Id { get; set; }
        public int OrderLocation { get; set; }
        public int OrderCustomer { get; set; }
        public DateTime OrderTime { get; set; }

        public static bool CheckCupcakeQuantity(int qnty)
        {
            return qnty <= 500 && qnty > 0;
        }

        public decimal GetTotalCost(List<Project1.BLL.OrderItem> orderItems, List<Project1.BLL.Cupcake> cupcakes)
        {
            decimal sum = 0;
            foreach (var orderItem in orderItems)
            {
                sum += (orderItem.Quantity ?? 0) * cupcakes.Single(c => c.Id == orderItem.CupcakeId).Cost;
            }
            return sum;
        }
    }
}
