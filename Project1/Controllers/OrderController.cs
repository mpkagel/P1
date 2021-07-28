using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project1.BLL.IDataRepos;
using Project1.ViewModels;
using P1B = Project1.BLL;

namespace Project1.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ICustomerRepo customerRepo, ILocationRepo locationRepo,
            IOrderRepo orderRepo, ICupcakeRepo cupcakeRepo, IOrderItemRepo orderItemRepo,
            ILocationInventoryRepo locationInventoryRepo, IRecipeItemRepo recipeItemRepo,
            ILogger<OrderController> logger)
        {
            LocRepo = locationRepo;
            CustomerRepo = customerRepo;
            OrderRepo = orderRepo;
            CupcakeRepo = cupcakeRepo;
            OrderItemRepo = orderItemRepo;
            LocationInventoryRepo = locationInventoryRepo;
            RecipeItemRepo = recipeItemRepo;

            _logger = logger;
        }

        public ILocationRepo LocRepo { get; set; }
        public ICustomerRepo CustomerRepo { get; set; }
        public IOrderRepo OrderRepo { get; set; }
        public ICupcakeRepo CupcakeRepo { get; set; }
        public IOrderItemRepo OrderItemRepo { get; set; }
        public ILocationInventoryRepo LocationInventoryRepo { get; set; }
        public IRecipeItemRepo RecipeItemRepo { get; set; }

        public ActionResult Index(string sortOrder)
        {
            ViewData["TimeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "time_desc" : "";
            ViewData["OrderTotalSortParm"] = sortOrder == "OrderTotal" ? "order_total_desc" : "OrderTotal";

            IEnumerable<P1B.Order> orders = OrderRepo.GetAllOrders();
            IEnumerable<P1B.Customer> customers = CustomerRepo.GetAllCustomers();
            IEnumerable<P1B.Location> locations = LocRepo.GetAllLocations();
            IEnumerable<P1B.Cupcake> cupcakes = CupcakeRepo.GetAllCupcakes().OrderBy(c => c.Id);

            switch (sortOrder)
            {
                case "time_desc":
                    orders = orders.OrderByDescending(o => o.OrderTime);
                    break;
                case "OrderTotal":
                    orders = orders.OrderBy(o => o.GetTotalCost(OrderItemRepo.GetOrderItems(o.Id).ToList(),
                        cupcakes.ToList()));
                    break;
                case "order_total_desc":
                    orders = orders.OrderByDescending(o => o.GetTotalCost(OrderItemRepo.GetOrderItems(o.Id).ToList(),
                        cupcakes.ToList()));
                    break;
                default:
                    orders = orders.OrderBy(o => o.OrderTime);
                    break;
            }

            var viewModels = orders.Select(o => new OrderViewModel
            {
                OrderId = o.Id,
                LocationId = o.OrderLocation,
                LocationName = locations.Single(l => l.Id == o.OrderLocation).Name,
                CustomerId = o.OrderCustomer,
                CustomerName = customers.Single(c => c.Id == o.OrderCustomer).ReturnFullName(),
                OrderTime = o.OrderTime,
                Locations = locations.ToList(),
                Customers = customers.ToList(),
                Cupcakes = cupcakes.ToList(),
                OrderItems = OrderItemRepo.GetOrderItems(o.Id).ToList(),
                OrderTotal = OrderRepo.GetOrder(o.Id).GetTotalCost(OrderItemRepo.GetOrderItems(o.Id).ToList(),
                                        cupcakes.ToList())
            }).ToList();

            return View(viewModels);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            IEnumerable<P1B.Customer> customers = CustomerRepo.GetAllCustomers();
            IEnumerable<P1B.Location> locations = LocRepo.GetAllLocations();
            List<Project1.BLL.Cupcake> cupcakes = CupcakeRepo.GetAllCupcakes().OrderBy(c => c.Id).ToList();
            List<Project1.BLL.OrderItem> orderItems = OrderItemRepo.GetOrderItems(id).ToList();
            Project1.BLL.Order order = OrderRepo.GetOrder(id);

            var viewModel = new OrderViewModel
            {
                OrderId = id,
                LocationId = order.OrderLocation,
                LocationName = locations.Single(l => l.Id == order.OrderLocation).Name,
                CustomerId = order.OrderCustomer,
                CustomerName = customers.Single(c => c.Id == order.OrderCustomer).ReturnFullName(),
                OrderTime = order.OrderTime,
                Locations = LocRepo.GetAllLocations().ToList(),
                Customers = CustomerRepo.GetAllCustomers().ToList(),
                Cupcakes = cupcakes,
                OrderItems = orderItems,
                OrderTotal = OrderRepo.GetOrder(order.Id).GetTotalCost(OrderItemRepo.GetOrderItems(order.Id).ToList(),
                                        cupcakes.ToList())
            };
            // give the Create view values for its dropdown
            return View(viewModel);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            List<Project1.BLL.Cupcake> cupcakesTemp = CupcakeRepo.GetAllCupcakes().OrderBy(c => c.Id).ToList();
            List<Project1.BLL.OrderItem> orderItemsTemp = new List<Project1.BLL.OrderItem>();
            foreach (var cupcake in cupcakesTemp)
            {
                cupcake.Type = Regex.Replace(cupcake.Type, "([a-z])([A-Z])", "$1 $2");
                orderItemsTemp.Add(new Project1.BLL.OrderItem
                {
                    Id = 0,
                    OrderId = 0,
                    CupcakeId = cupcake.Id,
                    Quantity = null
                });
            }

            var viewModel = new OrderViewModel
            {
                Locations = LocRepo.GetAllLocations().ToList(),
                Customers = CustomerRepo.GetAllCustomers().ToList(),
                Cupcakes = cupcakesTemp,
                OrderItems = orderItemsTemp
            };
            foreach (Project1.BLL.Customer customer in viewModel.Customers)
            {
                customer.FullName = customer.ReturnFullName();
            }

            // give the Create view values for its dropdown
            return View(viewModel);
        }

        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project1.ViewModels.OrderViewModel viewModel)
        {
            try
            {
                for (int i = 0; i < viewModel.OrderItems.Count; i++)
                {
                    viewModel.OrderItems[i].CupcakeId = i + 1;
                }
                List<Project1.BLL.OrderItem> newOrderItems = viewModel.OrderItems
                                                    .Where(oi => oi.Quantity != null).ToList();
                if (newOrderItems.Count == 0)
                {
                    string message = "The order must have at least one cupcake.";
                    TempData["ErrorMessage"] = message;
                    _logger.LogWarning(message);
                    return RedirectToAction("Error", "Home");
                }

                viewModel.Locations = LocRepo.GetAllLocations().ToList();
                viewModel.Customers = CustomerRepo.GetAllCustomers().ToList();
                viewModel.Cupcakes = CupcakeRepo.GetAllCupcakes().ToList();
                viewModel.CustomerName = viewModel.Customers.Single(c => c.Id == viewModel.CustomerId).ReturnFullName();
                viewModel.LocationName = viewModel.Locations.Single(l => l.Id == viewModel.LocationId).Name;

                if (!CustomerRepo.CheckCustomerExists(viewModel.CustomerId))
                {
                    string message = $"Customer {viewModel.CustomerName} is not in the database.";
                    TempData["ErrorMessage"] = message;
                    _logger.LogWarning(message);
                    return RedirectToAction("Error", "Home");
                }
                if (!LocRepo.CheckLocationExists(viewModel.LocationId))
                {
                    string message = $"Location {viewModel.LocationName} is not in the list of stores.";
                    TempData["ErrorMessage"] = message;
                    _logger.LogWarning(message);
                    return RedirectToAction("Error", "Home");
                }
                // The following checks to see if the customer can order from this store location
                // If the customer has ordered at this store within the past 2 hours, than they shouldn't be
                // able to order again.
                var orders = OrderRepo.GetAllOrders().ToList();
                if (!Project1.BLL.Customer.CheckCustomerCanOrder(viewModel.CustomerId,
                    viewModel.LocationId, orders))
                {
                    string message = "Customer can't place an order at this store because it hasn't been 2 hours \n" +
                        "since there last order yet.";
                    TempData["ErrorMessage"] = message;
                    _logger.LogWarning(message);
                    return RedirectToAction("Error", "Home");
                }
                foreach (var cupcake in newOrderItems)
                {
                    if (!CupcakeRepo.CheckCupcakeExists(cupcake.CupcakeId))
                    {
                        string message = $"Cupcake {viewModel.Cupcakes.Single(c => c.Id == cupcake.CupcakeId).Type} " +
                            $"is not in the database";
                        TempData["ErrorMessage"] = message;
                        _logger.LogWarning(message);
                        return RedirectToAction("Error", "Home");
                    }
                    int qnty = cupcake.Quantity ?? 0;
                    if (!Project1.BLL.Order.CheckCupcakeQuantity(qnty))
                    {
                        string message = $"Quantity for cupcake " +
                            $"{viewModel.Cupcakes.Single(c => c.Id == cupcake.CupcakeId).Type} " +
                            $"is out of range";
                        TempData["ErrorMessage"] = message;
                        _logger.LogWarning(message);
                        return RedirectToAction("Error", "Home");
                    }
                }
                // The following gets all orders and their associated order items in order
                // to validate that the store location's supply of the cupcakes in the customer's
                // requested order have not been exhausted.
                // Cupcake exhaustion happens when a store location has had more than 1000 cupcakes of one
                // type sold within 24 hours, at that point they cannot sell anymore.
                // This is arbitrary business logic that I added in order to satisfy the Project0
                // requirements.
                var orderItems = OrderItemRepo.GetAllOrderItems().ToList();
                if (!Project1.BLL.Location.CheckCanOrderCupcake(viewModel.LocationId,
                    orders, orderItems, newOrderItems))
                {
                    string message = $"This store has exhausted supply of one of those cupcakes. " +
                        $"Try back in 24 hours.";
                    TempData["ErrorMessage"] = message;
                    _logger.LogWarning(message);
                    return RedirectToAction("Error", "Home");
                }
                // The following gets the recipes for the cupcakes in the customer's requested order
                // and checks to make sure that the store location's inventory can support the order.
                var recipes = RecipeItemRepo.GetRecipes(newOrderItems);
                var locationInv = LocationInventoryRepo.GetLocationInventoryByLocationId(viewModel.LocationId).ToList();
                if (!Project1.BLL.Location.CheckOrderFeasible(recipes, locationInv, newOrderItems))
                {
                    string message = $"This store does not have enough ingredients to place " +
                        $"the requested order.";
                    TempData["ErrorMessage"] = message;
                    _logger.LogWarning(message);
                    return RedirectToAction("Error", "Home");
                }

                var newOrder = new P1B.Order
                {
                    OrderLocation = viewModel.LocationId,
                    OrderCustomer = viewModel.CustomerId,
                    OrderTime = DateTime.Now
                };
                OrderRepo.AddCupcakeOrder(newOrder);

                int newOrderId = OrderRepo.GetLastCupcakeOrderAdded();
                for (int i = 0; i < newOrderItems.Count; i++)
                {
                    newOrderItems[i].OrderId = newOrderId;
                }

                OrderItemRepo.AddCupcakeOrderItems(newOrderItems);
                LocationInventoryRepo.UpdateLocationInv(viewModel.LocationId, recipes, newOrderItems);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error", "Home");
            }
        }

        
    }
}