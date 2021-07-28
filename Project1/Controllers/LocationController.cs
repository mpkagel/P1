using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1.BLL.IDataRepos;
using P1B = Project1.BLL;
using Project1.ViewModels;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace Project1.Controllers
{
    public class LocationController : Controller
    {
        private readonly ILogger<LocationController> _logger;

        public LocationController(ICustomerRepo customerRepo, ILocationRepo locationRepo,
            IOrderRepo orderRepo, ICupcakeRepo cupcakeRepo, IOrderItemRepo orderItemRepo,
            ILocationInventoryRepo locationInventoryRepo, IRecipeItemRepo recipeItemRepo,
            IIngredientRepo ingRepo, ILogger<LocationController> logger)
        {
            LocRepo = locationRepo;
            CustomerRepo = customerRepo;
            OrderRepo = orderRepo;
            CupcakeRepo = cupcakeRepo;
            OrderItemRepo = orderItemRepo;
            LocationInventoryRepo = locationInventoryRepo;
            RecipeItemRepo = recipeItemRepo;
            IngRepo = ingRepo;

            _logger = logger;
        }

        public ILocationRepo LocRepo { get; set; }
        public ICustomerRepo CustomerRepo { get; set; }
        public IOrderRepo OrderRepo { get; set; }
        public ICupcakeRepo CupcakeRepo { get; set; }
        public IOrderItemRepo OrderItemRepo { get; set; }
        public ILocationInventoryRepo LocationInventoryRepo { get; set; }
        public IRecipeItemRepo RecipeItemRepo { get; set; }
        public IIngredientRepo IngRepo { get; set; }

        // GET: Location
        public ActionResult Index()
        {
            IEnumerable<P1B.Location> locations = LocRepo.GetAllLocations();
            return View(locations);
        }

        // GET: Location/Details/5
        [Route("[controller]/Inventory/{id?}")]
        public ActionResult Inventory(int id)
        {
            P1B.Location currentLocation = LocRepo.GetLocationById(id);
            ViewData["currentLocation"] = currentLocation.Name;

            IEnumerable<P1B.LocationInventory> locInvs = LocationInventoryRepo.GetLocationInventoryByLocationId(id)
                .OrderBy(li => li.IngredientId);
            List<P1B.Ingredient> ings = IngRepo.GetIngredients().ToList();

            var viewModels = locInvs.Select(li => new LocationInventoryViewModel
            {
                LocationId = id,
                LocationName = currentLocation.Name,
                IngredientId = li.IngredientId,
                // https://stackoverflow.com/questions/272633/add-spaces-before-capital-letters
                IngredientType = Regex.Replace(ings.Single(i => i.Id == li.IngredientId).Type, "([a-z])([A-Z])", "$1 $2"),
                IngredientUnits = ings.Single(i => i.Id == li.IngredientId).Units,
                IngredientAmount = li.Amount
            }).ToList();

            return View(viewModels);
        }

        // GET: Order/Details/5
        public ActionResult Orders(int id, string sortOrder)
        {
            ViewData["TimeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "time_desc" : "";
            ViewData["OrderTotalSortParm"] = sortOrder == "OrderTotal" ? "order_total_desc" : "OrderTotal";

            IEnumerable<P1B.Customer> customers = CustomerRepo.GetAllCustomers();
            IEnumerable<P1B.Location> locations = LocRepo.GetAllLocations();
            List<Project1.BLL.Cupcake> cupcakes = CupcakeRepo.GetAllCupcakes().OrderBy(c => c.Id).ToList();
            IEnumerable<P1B.Order> orders = LocRepo.GetLocationOrderHistory(id).ToList();
            List<OrderViewModel> viewModels = new List<OrderViewModel>();

            ViewData["LocationName"] = locations.Single(l => l.Id == id).Name;
            ViewData["LocationId"] = id;


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

            foreach (var order in orders)
            {
                viewModels.Add(new OrderViewModel
                {
                    OrderId = order.Id,
                    LocationId = order.OrderLocation,
                    LocationName = locations.Single(l => l.Id == order.OrderLocation).Name,
                    CustomerId = order.OrderCustomer,
                    CustomerName = customers.Single(c => c.Id == order.OrderCustomer).ReturnFullName(),
                    OrderTime = order.OrderTime,
                    Locations = locations.ToList(),
                    Customers = customers.ToList(),
                    Cupcakes = cupcakes,
                    OrderItems = OrderItemRepo.GetOrderItems(order.Id).ToList(),
                    OrderTotal = OrderRepo.GetOrder(order.Id).GetTotalCost(OrderItemRepo.GetOrderItems(order.Id).ToList(),
                                        cupcakes.ToList())
                });
            }

            return View(viewModels);
        }

        // GET: Order/Details/5
        public ActionResult Order(int id)
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

        // GET: Location/Create
        public ActionResult Create()
        {
            Project1.ViewModels.LocationViewModel viewModel = new LocationViewModel();
            return View(viewModel);
        }

        // POST: Location/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project1.ViewModels.LocationViewModel viewModel)
        {
            try
            {
                if (viewModel.LocationName.Length == 0)
                {
                    string message = "The location name cannot be empty.";
                    TempData["ErrorMessage"] = message;
                    _logger.LogWarning(message);
                    return RedirectToAction("Error", "Home");
                }
                if (LocRepo.CheckLocationNameExists(viewModel.LocationName))
                {
                    string message = "This location name has already been used in the database.";
                    TempData["ErrorMessage"] = message;
                    _logger.LogWarning(message);
                    return RedirectToAction("Error", "Home");
                }

                // TODO: Add insert logic here
                var newLocation = new P1B.Location
                {
                    Name = viewModel.LocationName
                };

                // TODO: Add insert logic here
                LocRepo.AddLocation(newLocation);
                int newLocationId = LocRepo.GetLastLocationAdded();
                LocationInventoryRepo.FillLocationInventory(newLocationId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: Location/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Location/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Location/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Location/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}