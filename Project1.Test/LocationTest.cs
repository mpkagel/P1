using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using P1B = Project1.BLL;

namespace Project1.Test
{
    public class LocationTest
    {
        [Fact]
        public void TestCheckCanOrderCupcakeTrue()
        {
            // Arrange
            int locationId = 1;

            List<P1B.OrderItem> newOrderItems = new List<P1B.OrderItem>
            {
                new P1B.OrderItem
                {
                    Id = 1,
                    OrderId = 0,
                    CupcakeId = 1,
                    Quantity = 6
                },
                new P1B.OrderItem
                {
                    Id = 2,
                    OrderId = 0,
                    CupcakeId = 4,
                    Quantity = 80
                },
                new P1B.OrderItem
                {
                    Id = 3,
                    OrderId = 0,
                    CupcakeId = 7,
                    Quantity = 75
                },
            };

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

            List<P1B.OrderItem> orderItems = new List<P1B.OrderItem>
            {
                new P1B.OrderItem
                {
                    Id = 1,
                    OrderId = 1,
                    CupcakeId = 1,
                    Quantity = 400
                },
                new P1B.OrderItem
                {
                    Id = 2,
                    OrderId = 1,
                    CupcakeId = 4,
                    Quantity = 400
                },
                new P1B.OrderItem
                {
                    Id = 3,
                    OrderId = 1,
                    CupcakeId = 7,
                    Quantity = 400
                },
            };

            // Act and Assert
            Assert.True(P1B.Location.CheckCanOrderCupcake(locationId,
                orders, orderItems, newOrderItems));
        }

        [Fact]
        public void TestCheckCanOrderCupcakeFalse()
        {
            // Arrange
            int locationId = 1;

            List<P1B.OrderItem> newOrderItems = new List<P1B.OrderItem>
            {
                new P1B.OrderItem
                {
                    Id = 1,
                    OrderId = 0,
                    CupcakeId = 1,
                    Quantity = 6
                },
                new P1B.OrderItem
                {
                    Id = 2,
                    OrderId = 0,
                    CupcakeId = 4,
                    Quantity = 80
                },
                new P1B.OrderItem
                {
                    Id = 3,
                    OrderId = 0,
                    CupcakeId = 7,
                    Quantity = 75
                },
            };

            List<P1B.Order> orders = new List<P1B.Order>
            {
                new P1B.Order
                {
                    Id = 1,
                    OrderLocation = 1,
                    OrderCustomer = 1,
                    OrderTime = DateTime.Now
                },
                new P1B.Order
                {
                    Id = 2,
                    OrderLocation = 1,
                    OrderCustomer = 2,
                    OrderTime = DateTime.Now
                },
                new P1B.Order
                {
                    Id = 3,
                    OrderLocation = 1,
                    OrderCustomer = 3,
                    OrderTime = DateTime.Now
                }
            };

            List<P1B.OrderItem> orderItems = new List<P1B.OrderItem>
            {
                new P1B.OrderItem
                {
                    Id = 1,
                    OrderId = 1,
                    CupcakeId = 1,
                    Quantity = 400
                },
                new P1B.OrderItem
                {
                    Id = 2,
                    OrderId = 1,
                    CupcakeId = 4,
                    Quantity = 400
                },
                new P1B.OrderItem
                {
                    Id = 3,
                    OrderId = 1,
                    CupcakeId = 7,
                    Quantity = 400
                },
                new P1B.OrderItem
                {
                    Id = 4,
                    OrderId = 1,
                    CupcakeId = 1,
                    Quantity = 400
                },
                new P1B.OrderItem
                {
                    Id = 5,
                    OrderId = 1,
                    CupcakeId = 4,
                    Quantity = 400
                },
                new P1B.OrderItem
                {
                    Id = 6,
                    OrderId = 1,
                    CupcakeId = 7,
                    Quantity = 400
                },
                new P1B.OrderItem
                {
                    Id = 7,
                    OrderId = 1,
                    CupcakeId = 1,
                    Quantity = 400
                },
                new P1B.OrderItem
                {
                    Id = 8,
                    OrderId = 1,
                    CupcakeId = 4,
                    Quantity = 400
                },
                new P1B.OrderItem
                {
                    Id = 9,
                    OrderId = 1,
                    CupcakeId = 7,
                    Quantity = 400
                },
            };

            // Act and Assert
            Assert.False(P1B.Location.CheckCanOrderCupcake(locationId,
                orders, orderItems, newOrderItems));
        }

        [Fact]
        public void TestCheckCanOrderCupcakeTrue24HoursLater()
        {
            // Arrange
            int locationId = 1;

            List<P1B.OrderItem> newOrderItems = new List<P1B.OrderItem>
            {
                new P1B.OrderItem
                {
                    Id = 1,
                    OrderId = 0,
                    CupcakeId = 1,
                    Quantity = 6
                },
                new P1B.OrderItem
                {
                    Id = 2,
                    OrderId = 0,
                    CupcakeId = 4,
                    Quantity = 80
                },
                new P1B.OrderItem
                {
                    Id = 3,
                    OrderId = 0,
                    CupcakeId = 7,
                    Quantity = 75
                },
            };

            List<P1B.Order> orders = new List<P1B.Order>
            {
                new P1B.Order
                {
                    Id = 1,
                    OrderLocation = 1,
                    OrderCustomer = 1,
                    OrderTime = DateTime.Now.AddMinutes(-1441)
                },
                new P1B.Order
                {
                    Id = 2,
                    OrderLocation = 1,
                    OrderCustomer = 2,
                    OrderTime = DateTime.Now.AddMinutes(-1441)
                },
                new P1B.Order
                {
                    Id = 3,
                    OrderLocation = 1,
                    OrderCustomer = 3,
                    OrderTime = DateTime.Now.AddMinutes(-1441)
                }
            };

            List<P1B.OrderItem> orderItems = new List<P1B.OrderItem>
            {
                new P1B.OrderItem
                {
                    Id = 1,
                    OrderId = 1,
                    CupcakeId = 1,
                    Quantity = 400
                },
                new P1B.OrderItem
                {
                    Id = 2,
                    OrderId = 1,
                    CupcakeId = 4,
                    Quantity = 400
                },
                new P1B.OrderItem
                {
                    Id = 3,
                    OrderId = 1,
                    CupcakeId = 7,
                    Quantity = 400
                },
                new P1B.OrderItem
                {
                    Id = 4,
                    OrderId = 1,
                    CupcakeId = 1,
                    Quantity = 400
                },
                new P1B.OrderItem
                {
                    Id = 5,
                    OrderId = 1,
                    CupcakeId = 4,
                    Quantity = 400
                },
                new P1B.OrderItem
                {
                    Id = 6,
                    OrderId = 1,
                    CupcakeId = 7,
                    Quantity = 400
                },
                new P1B.OrderItem
                {
                    Id = 7,
                    OrderId = 1,
                    CupcakeId = 1,
                    Quantity = 400
                },
                new P1B.OrderItem
                {
                    Id = 8,
                    OrderId = 1,
                    CupcakeId = 4,
                    Quantity = 400
                },
                new P1B.OrderItem
                {
                    Id = 9,
                    OrderId = 1,
                    CupcakeId = 7,
                    Quantity = 400
                },
            };

            // Act and Assert
            Assert.True(P1B.Location.CheckCanOrderCupcake(locationId,
                orders, orderItems, newOrderItems));
        }

        [Fact]
        public void TestCheckOrderFeasibleInvGreaterTrue()
        {
            // Arrange
            List<P1B.OrderItem> newOrderItems = new List<P1B.OrderItem>
            {
                new P1B.OrderItem
                {
                    Id = 1,
                    OrderId = 0,
                    CupcakeId = 1,
                    Quantity = 1
                },
                new P1B.OrderItem
                {
                    Id = 2,
                    OrderId = 0,
                    CupcakeId = 4,
                    Quantity = 1
                },
                new P1B.OrderItem
                {
                    Id = 3,
                    OrderId = 0,
                    CupcakeId = 7,
                    Quantity = 1
                },
            };

            Dictionary<int, Dictionary<int, decimal>> recipes = new Dictionary<int, Dictionary<int, decimal>>
            {
                {
                    1,
                    new Dictionary<int, decimal>
                    {
                        { 1, 1 },
                        { 2, 1 },
                        { 3, 1 },
                        { 4, 1 },
                        { 5, 1 },
                        { 6, 1 },
                        { 7, 1 },
                        { 8, 1 },
                        { 9, 1 },
                        { 10, 1 },
                        { 11, 1 },
                        { 12, 1 },
                        { 13, 1 },
                        { 14, 1 },
                        { 15, 1 },
                        { 16, 1 },
                        { 17, 1 },
                        { 18, 1 }
                    }
                },
                {
                    4,
                    new Dictionary<int, decimal>
                    {
                        { 1, 2 },
                        { 2, 2 },
                        { 3, 2 },
                        { 4, 2 },
                        { 5, 2 },
                        { 6, 2 },
                        { 7, 2 },
                        { 8, 2 },
                        { 9, 2 },
                        { 10, 2 },
                        { 11, 2 },
                        { 12, 2 },
                        { 13, 2 },
                        { 14, 2 },
                        { 15, 2 },
                        { 16, 2 },
                        { 17, 2 },
                        { 18, 2 }
                    }
                },
                {
                    7,
                    new Dictionary<int, decimal>
                    {
                        { 1, 3 },
                        { 2, 3 },
                        { 3, 3 },
                        { 4, 3 },
                        { 5, 3 },
                        { 6, 3 },
                        { 7, 3 },
                        { 8, 3 },
                        { 9, 3 },
                        { 10, 3 },
                        { 11, 3 },
                        { 12, 3 },
                        { 13, 3 },
                        { 14, 3 },
                        { 15, 3 },
                        { 16, 3 },
                        { 17, 3 },
                        { 18, 3 }
                    }
                }
            };

            List<P1B.LocationInventory> locationInv = new List<P1B.LocationInventory>
            {
                new P1B.LocationInventory
                {
                    Id = 1,
                    LocationId = 0,
                    IngredientId = 1,
                    Amount = 8
                },
                new P1B.LocationInventory
                {
                    Id = 2,
                    LocationId = 0,
                    IngredientId = 2,
                    Amount = 8
                },
                new P1B.LocationInventory
                {
                    Id = 3,
                    LocationId = 0,
                    IngredientId = 3,
                    Amount = 8
                },
                new P1B.LocationInventory
                {
                    Id = 4,
                    LocationId = 0,
                    IngredientId = 4,
                    Amount = 8
                },
                new P1B.LocationInventory
                {
                    Id = 5,
                    LocationId = 0,
                    IngredientId = 5,
                    Amount = 8
                },
                new P1B.LocationInventory
                {
                    Id = 6,
                    LocationId = 0,
                    IngredientId = 6,
                    Amount = 8
                },
                new P1B.LocationInventory
                {
                    Id = 7,
                    LocationId = 0,
                    IngredientId = 7,
                    Amount = 8
                },
                new P1B.LocationInventory
                {
                    Id = 8,
                    LocationId = 0,
                    IngredientId = 8,
                    Amount = 8
                },
                new P1B.LocationInventory
                {
                    Id = 9,
                    LocationId = 0,
                    IngredientId = 9,
                    Amount = 8
                },
                new P1B.LocationInventory
                {
                    Id = 10,
                    LocationId = 0,
                    IngredientId = 10,
                    Amount = 8
                },
                new P1B.LocationInventory
                {
                    Id = 11,
                    LocationId = 0,
                    IngredientId = 11,
                    Amount = 8
                },
                new P1B.LocationInventory
                {
                    Id = 12,
                    LocationId = 0,
                    IngredientId = 12,
                    Amount = 8
                },
                new P1B.LocationInventory
                {
                    Id = 13,
                    LocationId = 0,
                    IngredientId = 13,
                    Amount = 8
                },
                new P1B.LocationInventory
                {
                    Id = 14,
                    LocationId = 0,
                    IngredientId = 14,
                    Amount = 8
                },
                new P1B.LocationInventory
                {
                    Id = 15,
                    LocationId = 0,
                    IngredientId = 15,
                    Amount = 8
                },
                new P1B.LocationInventory
                {
                    Id = 16,
                    LocationId = 0,
                    IngredientId = 16,
                    Amount = 8
                },
                new P1B.LocationInventory
                {
                    Id = 17,
                    LocationId = 0,
                    IngredientId = 17,
                    Amount = 8
                },
                new P1B.LocationInventory
                {
                    Id = 18,
                    LocationId = 0,
                    IngredientId = 18,
                    Amount = 8
                }
            };

            // Act and Assert
            Assert.True(P1B.Location.CheckOrderFeasible(recipes, locationInv, newOrderItems));
        }

        [Fact]
        public void TestCheckOrderFeasibleInvEqualTrue()
        {
            // Arrange
            List<P1B.OrderItem> newOrderItems = new List<P1B.OrderItem>
            {
                new P1B.OrderItem
                {
                    Id = 1,
                    OrderId = 0,
                    CupcakeId = 1,
                    Quantity = 1
                },
                new P1B.OrderItem
                {
                    Id = 2,
                    OrderId = 0,
                    CupcakeId = 4,
                    Quantity = 1
                },
                new P1B.OrderItem
                {
                    Id = 3,
                    OrderId = 0,
                    CupcakeId = 7,
                    Quantity = 1
                },
            };

            Dictionary<int, Dictionary<int, decimal>> recipes = new Dictionary<int, Dictionary<int, decimal>>
            {
                {
                    1,
                    new Dictionary<int, decimal>
                    {
                        { 1, 1 },
                        { 2, 1 },
                        { 3, 1 },
                        { 4, 1 },
                        { 5, 1 },
                        { 6, 1 },
                        { 7, 1 },
                        { 8, 1 },
                        { 9, 1 },
                        { 10, 1 },
                        { 11, 1 },
                        { 12, 1 },
                        { 13, 1 },
                        { 14, 1 },
                        { 15, 1 },
                        { 16, 1 },
                        { 17, 1 },
                        { 18, 1 }
                    }
                },
                {
                    4,
                    new Dictionary<int, decimal>
                    {
                        { 1, 2 },
                        { 2, 2 },
                        { 3, 2 },
                        { 4, 2 },
                        { 5, 2 },
                        { 6, 2 },
                        { 7, 2 },
                        { 8, 2 },
                        { 9, 2 },
                        { 10, 2 },
                        { 11, 2 },
                        { 12, 2 },
                        { 13, 2 },
                        { 14, 2 },
                        { 15, 2 },
                        { 16, 2 },
                        { 17, 2 },
                        { 18, 2 }
                    }
                },
                {
                    7,
                    new Dictionary<int, decimal>
                    {
                        { 1, 3 },
                        { 2, 3 },
                        { 3, 3 },
                        { 4, 3 },
                        { 5, 3 },
                        { 6, 3 },
                        { 7, 3 },
                        { 8, 3 },
                        { 9, 3 },
                        { 10, 3 },
                        { 11, 3 },
                        { 12, 3 },
                        { 13, 3 },
                        { 14, 3 },
                        { 15, 3 },
                        { 16, 3 },
                        { 17, 3 },
                        { 18, 3 }
                    }
                }
            };

            List<P1B.LocationInventory> locationInv = new List<P1B.LocationInventory>
            {
                new P1B.LocationInventory
                {
                    Id = 1,
                    LocationId = 0,
                    IngredientId = 1,
                    Amount = 6
                },
                new P1B.LocationInventory
                {
                    Id = 2,
                    LocationId = 0,
                    IngredientId = 2,
                    Amount = 6
                },
                new P1B.LocationInventory
                {
                    Id = 3,
                    LocationId = 0,
                    IngredientId = 3,
                    Amount = 6
                },
                new P1B.LocationInventory
                {
                    Id = 4,
                    LocationId = 0,
                    IngredientId = 4,
                    Amount = 6
                },
                new P1B.LocationInventory
                {
                    Id = 5,
                    LocationId = 0,
                    IngredientId = 5,
                    Amount = 6
                },
                new P1B.LocationInventory
                {
                    Id = 6,
                    LocationId = 0,
                    IngredientId = 6,
                    Amount = 6
                },
                new P1B.LocationInventory
                {
                    Id = 7,
                    LocationId = 0,
                    IngredientId = 7,
                    Amount = 6
                },
                new P1B.LocationInventory
                {
                    Id = 8,
                    LocationId = 0,
                    IngredientId = 8,
                    Amount = 6
                },
                new P1B.LocationInventory
                {
                    Id = 9,
                    LocationId = 0,
                    IngredientId = 9,
                    Amount = 6
                },
                new P1B.LocationInventory
                {
                    Id = 10,
                    LocationId = 0,
                    IngredientId = 10,
                    Amount = 6
                },
                new P1B.LocationInventory
                {
                    Id = 11,
                    LocationId = 0,
                    IngredientId = 11,
                    Amount = 6
                },
                new P1B.LocationInventory
                {
                    Id = 12,
                    LocationId = 0,
                    IngredientId = 12,
                    Amount = 6
                },
                new P1B.LocationInventory
                {
                    Id = 13,
                    LocationId = 0,
                    IngredientId = 13,
                    Amount = 6
                },
                new P1B.LocationInventory
                {
                    Id = 14,
                    LocationId = 0,
                    IngredientId = 14,
                    Amount = 6
                },
                new P1B.LocationInventory
                {
                    Id = 15,
                    LocationId = 0,
                    IngredientId = 15,
                    Amount = 6
                },
                new P1B.LocationInventory
                {
                    Id = 16,
                    LocationId = 0,
                    IngredientId = 16,
                    Amount = 6
                },
                new P1B.LocationInventory
                {
                    Id = 17,
                    LocationId = 0,
                    IngredientId = 17,
                    Amount = 6
                },
                new P1B.LocationInventory
                {
                    Id = 18,
                    LocationId = 0,
                    IngredientId = 18,
                    Amount = 6
                }
            };

            // Act and Assert
            Assert.True(P1B.Location.CheckOrderFeasible(recipes, locationInv, newOrderItems));
        }

        [Fact]
        public void TestCheckOrderFeasibleInvLessFalse()
        {
            // Arrange
            List<P1B.OrderItem> newOrderItems = new List<P1B.OrderItem>
            {
                new P1B.OrderItem
                {
                    Id = 1,
                    OrderId = 0,
                    CupcakeId = 1,
                    Quantity = 1
                },
                new P1B.OrderItem
                {
                    Id = 2,
                    OrderId = 0,
                    CupcakeId = 4,
                    Quantity = 1
                },
                new P1B.OrderItem
                {
                    Id = 3,
                    OrderId = 0,
                    CupcakeId = 7,
                    Quantity = 1
                },
            };

            Dictionary<int, Dictionary<int, decimal>> recipes = new Dictionary<int, Dictionary<int, decimal>>
            {
                {
                    1,
                    new Dictionary<int, decimal>
                    {
                        { 1, 1 },
                        { 2, 1 },
                        { 3, 1 },
                        { 4, 1 },
                        { 5, 1 },
                        { 6, 1 },
                        { 7, 1 },
                        { 8, 1 },
                        { 9, 1 },
                        { 10, 1 },
                        { 11, 1 },
                        { 12, 1 },
                        { 13, 1 },
                        { 14, 1 },
                        { 15, 1 },
                        { 16, 1 },
                        { 17, 1 },
                        { 18, 1 }
                    }
                },
                {
                    4,
                    new Dictionary<int, decimal>
                    {
                        { 1, 2 },
                        { 2, 2 },
                        { 3, 2 },
                        { 4, 2 },
                        { 5, 2 },
                        { 6, 2 },
                        { 7, 2 },
                        { 8, 2 },
                        { 9, 2 },
                        { 10, 2 },
                        { 11, 2 },
                        { 12, 2 },
                        { 13, 2 },
                        { 14, 2 },
                        { 15, 2 },
                        { 16, 2 },
                        { 17, 2 },
                        { 18, 2 }
                    }
                },
                {
                    7,
                    new Dictionary<int, decimal>
                    {
                        { 1, 3 },
                        { 2, 3 },
                        { 3, 3 },
                        { 4, 3 },
                        { 5, 3 },
                        { 6, 3 },
                        { 7, 3 },
                        { 8, 3 },
                        { 9, 3 },
                        { 10, 3 },
                        { 11, 3 },
                        { 12, 3 },
                        { 13, 3 },
                        { 14, 3 },
                        { 15, 3 },
                        { 16, 3 },
                        { 17, 3 },
                        { 18, 3 }
                    }
                }
            };

            List<P1B.LocationInventory> locationInv = new List<P1B.LocationInventory>
            {
                new P1B.LocationInventory
                {
                    Id = 1,
                    LocationId = 0,
                    IngredientId = 1,
                    Amount = 0
                },
                new P1B.LocationInventory
                {
                    Id = 2,
                    LocationId = 0,
                    IngredientId = 2,
                    Amount = 0
                },
                new P1B.LocationInventory
                {
                    Id = 3,
                    LocationId = 0,
                    IngredientId = 3,
                    Amount = 0
                },
                new P1B.LocationInventory
                {
                    Id = 4,
                    LocationId = 0,
                    IngredientId = 4,
                    Amount = 0
                },
                new P1B.LocationInventory
                {
                    Id = 5,
                    LocationId = 0,
                    IngredientId = 5,
                    Amount = 0
                },
                new P1B.LocationInventory
                {
                    Id = 6,
                    LocationId = 0,
                    IngredientId = 6,
                    Amount = 0
                },
                new P1B.LocationInventory
                {
                    Id = 7,
                    LocationId = 0,
                    IngredientId = 7,
                    Amount = 0
                },
                new P1B.LocationInventory
                {
                    Id = 8,
                    LocationId = 0,
                    IngredientId = 8,
                    Amount = 0
                },
                new P1B.LocationInventory
                {
                    Id = 9,
                    LocationId = 0,
                    IngredientId = 9,
                    Amount = 0
                },
                new P1B.LocationInventory
                {
                    Id = 10,
                    LocationId = 0,
                    IngredientId = 10,
                    Amount = 0
                },
                new P1B.LocationInventory
                {
                    Id = 11,
                    LocationId = 0,
                    IngredientId = 11,
                    Amount = 0
                },
                new P1B.LocationInventory
                {
                    Id = 12,
                    LocationId = 0,
                    IngredientId = 12,
                    Amount = 0
                },
                new P1B.LocationInventory
                {
                    Id = 13,
                    LocationId = 0,
                    IngredientId = 13,
                    Amount = 0
                },
                new P1B.LocationInventory
                {
                    Id = 14,
                    LocationId = 0,
                    IngredientId = 14,
                    Amount = 0
                },
                new P1B.LocationInventory
                {
                    Id = 15,
                    LocationId = 0,
                    IngredientId = 15,
                    Amount = 0
                },
                new P1B.LocationInventory
                {
                    Id = 16,
                    LocationId = 0,
                    IngredientId = 16,
                    Amount = 0
                },
                new P1B.LocationInventory
                {
                    Id = 17,
                    LocationId = 0,
                    IngredientId = 17,
                    Amount = 0
                },
                new P1B.LocationInventory
                {
                    Id = 18,
                    LocationId = 0,
                    IngredientId = 18,
                    Amount = 0
                }
            };

            // Act and Assert
            Assert.False(P1B.Location.CheckOrderFeasible(recipes, locationInv, newOrderItems));
        }
    }
}
