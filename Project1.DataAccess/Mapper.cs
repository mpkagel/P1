using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using P1B = Project1.BLL;
using DC = Project1.DataAccess;

namespace Project1.DataAccess
{
    public static class Mapper
    {
        // Location
        public static P1B.Location Map(DC.Location location) => new P1B.Location
        {
            Id = location.LocationId,
            Name = location.Name
        };
        public static DC.Location Map(P1B.Location location) => new DC.Location
        {
            LocationId = location.Id,
            Name = location.Name
        };
        public static IEnumerable<P1B.Location> Map(IEnumerable<DC.Location> locations) =>
            locations.Select(Map);
        public static IEnumerable<DC.Location> Map(IEnumerable<P1B.Location> locations) =>
            locations.Select(Map);

        // LocationInventory
        public static P1B.LocationInventory Map(DC.LocationInventory locInv) => new P1B.LocationInventory
        {
            Id = locInv.LocationInventoryId,
            LocationId = locInv.LocationId,
            IngredientId = locInv.IngredientId,
            Amount = locInv.Amount
        };
        public static DC.LocationInventory Map(P1B.LocationInventory locInv) => new DC.LocationInventory
        {
            LocationInventoryId = locInv.Id,
            LocationId = locInv.LocationId,
            IngredientId = locInv.IngredientId,
            Amount = locInv.Amount
        };
        public static IEnumerable<P1B.LocationInventory> Map(IEnumerable<DC.LocationInventory> locInvs) =>
            locInvs.Select(Map);
        public static IEnumerable<DC.LocationInventory> Map(IEnumerable<P1B.LocationInventory> locInvs) =>
            locInvs.Select(Map);

        // Ingredient
        public static P1B.Ingredient Map(DC.Ingredient ing) => new P1B.Ingredient
        {
            Id = ing.IngredientId,
            Type = ing.Type,
            Units = ing.Units
        };
        public static DC.Ingredient Map(P1B.Ingredient ing) => new DC.Ingredient
        {
            IngredientId = ing.Id,
            Type = ing.Type,
            Units = ing.Units
        };
        public static IEnumerable<P1B.Ingredient> Map(IEnumerable<DC.Ingredient> ings) =>
            ings.Select(Map);
        public static IEnumerable<DC.Ingredient> Map(IEnumerable<P1B.Ingredient> ings) =>
            ings.Select(Map);

        // Customer
        public static P1B.Customer Map(DC.Customer customer) => new P1B.Customer
        {
            Id = customer.CustomerId,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            DefaultLocation = customer.DefaultLocation
        };
        public static DC.Customer Map(P1B.Customer customer) => new DC.Customer
        {
            CustomerId = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            DefaultLocation = customer.DefaultLocation
        };
        public static IEnumerable<P1B.Customer> Map(IEnumerable<DC.Customer> customers) =>
            customers.Select(Map);
        public static IEnumerable<DC.Customer> Map(IEnumerable<P1B.Customer> customers) =>
            customers.Select(Map);

        // Cupcake
        public static P1B.Cupcake Map(DC.Cupcake cupcake) => new P1B.Cupcake
        {
            Id = cupcake.CupcakeId,
            Type = cupcake.Type,
            Cost = cupcake.Cost
        };
        public static DC.Cupcake Map(P1B.Cupcake cupcake) => new DC.Cupcake
        {
            CupcakeId = cupcake.Id,
            Type = cupcake.Type,
            Cost = cupcake.Cost
        };
        public static IEnumerable<P1B.Cupcake> Map(IEnumerable<DC.Cupcake> cupcakes) =>
            cupcakes.Select(Map);
        public static IEnumerable<DC.Cupcake> Map(IEnumerable<P1B.Cupcake> cupcakes) =>
            cupcakes.Select(Map);

        // CupcakeOrder
        public static P1B.Order Map(DC.CupcakeOrder order) => new P1B.Order
        {
            Id = order.OrderId,
            OrderLocation = order.LocationId,
            OrderCustomer = order.CustomerId,
            OrderTime = order.OrderTime
        };
        public static DC.CupcakeOrder Map(P1B.Order order) => new DC.CupcakeOrder
        {
            OrderId = order.Id,
            LocationId = order.OrderLocation,
            CustomerId = order.OrderCustomer,
            OrderTime = order.OrderTime
        };
        public static IEnumerable<P1B.Order> Map(IEnumerable<DC.CupcakeOrder> orders) =>
            orders.Select(Map);
        public static IEnumerable<DC.CupcakeOrder> Map(IEnumerable<P1B.Order> orders) =>
            orders.Select(Map);

        // CupcakeOrderItem
        public static P1B.OrderItem Map(DC.CupcakeOrderItem orderItem) => new P1B.OrderItem
        {
            Id = orderItem.CupcakeOrderItemId,
            OrderId = orderItem.OrderId,
            CupcakeId = orderItem.CupcakeId,
            Quantity = orderItem.Quantity
        };
        public static DC.CupcakeOrderItem Map(P1B.OrderItem orderItem) => new DC.CupcakeOrderItem
        {
            CupcakeOrderItemId = orderItem.Id,
            OrderId = orderItem.OrderId,
            CupcakeId = orderItem.CupcakeId,
            Quantity = orderItem.Quantity ?? 0
        };
        public static IEnumerable<P1B.OrderItem> Map(IEnumerable<DC.CupcakeOrderItem> orderItems) =>
            orderItems.Select(Map);
        public static IEnumerable<DC.CupcakeOrderItem> Map(IEnumerable<P1B.OrderItem> orderItems) =>
            orderItems.Select(Map);
    }
}
