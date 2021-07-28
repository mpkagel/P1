using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project1.BLL
{
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static bool CheckCanOrderCupcake(int locationId,
            List<Order> orders, List<OrderItem> orderItems, List<OrderItem> newOrderItems)
        {
            bool result = false;
            int sum = 0;
            foreach (var cupcake in newOrderItems)
            {
                // Get orders at store location
                var ordersAtStore = orders.Where(o => o.OrderLocation == locationId);
                // Get store location orders and find the ones within the past 24 hours
                var ordersAtStoreRecently =
                    ordersAtStore.Where(o =>
                    Math.Abs(o.OrderTime.Subtract(DateTime.Now).TotalMinutes) < 1440);
                foreach (var order in ordersAtStoreRecently)
                {
                    // Find the quantity of cupcakes in each order in the past 24 hours
                    var thisOrderItems = orderItems.Where(oi => oi.OrderId == order.Id);
                    foreach (var orderItem in thisOrderItems)
                    {
                        if (orderItem.CupcakeId == cupcake.CupcakeId)
                        {
                            sum += orderItem.Quantity ?? 0;
                        }
                    }
                }

                // If any of the cupcakes in the order have been exhausted: have orders causing
                // the quantity ordered to exceed 1000 in the past 24 hours, then the order
                // can't be placed.
                result = sum < 1000;
                sum = 0;
            }
            return result;
        }

        public static bool CheckOrderFeasible(Dictionary<int, Dictionary<int, decimal>> recipes,
            List<LocationInventory> locationInv, List<OrderItem> orderItems)
        {
            foreach (var cupcake in orderItems)
            {
                foreach (var ingredient in locationInv)
                {
                    ingredient.Amount -= 
                        recipes[cupcake.CupcakeId][ingredient.IngredientId] * (cupcake.Quantity ?? 0);
                }
            }

            foreach (var ingredient in locationInv)
            {
                if (ingredient.Amount < 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
