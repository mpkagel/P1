using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project1.BLL.IDataRepos;
using Project1.ViewModels;
using P1B = Project1.BLL;
using Microsoft.Extensions.Logging;

namespace Project1.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerRepo customerRepo, ILocationRepo locationRepo,
            IOrderItemRepo orderItemRepo, IOrderRepo orderRepo, ICupcakeRepo cupcakeRepo,
            ILogger<CustomerController> logger)
        {
            LocRepo = locationRepo;
            CustomerRepo = customerRepo;
            OrderItemRepo = orderItemRepo;
            OrderRepo = orderRepo;
            CupcakeRepo = cupcakeRepo;

            _logger = logger;
        }

        public ILocationRepo LocRepo { get; set; }
        public ICustomerRepo CustomerRepo { get; set; }
        public IOrderItemRepo OrderItemRepo { get; set; }
        public IOrderRepo OrderRepo { get; set; }
        public ICupcakeRepo CupcakeRepo { get; set; }

        // GET: Customer
        public ActionResult Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            IEnumerable<P1B.Customer> customers = CustomerRepo.GetAllCustomers();
            IEnumerable<P1B.Location> locations = LocRepo.GetAllLocations();

            if (!String.IsNullOrEmpty(searchString))
            {
                customers = customers.Where(c => c.FirstName.ToUpper().Contains(searchString.ToUpper())
                            || c.LastName.ToUpper().Contains(searchString.ToUpper()));
            }

            var viewModels = customers.Select(c => new CustomerViewModel
            {
                CustomerId = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                DefaultLocation = c.DefaultLocation ?? null,
                DefaultLocationName = c.DefaultLocation != null ? 
                (locations.Single(l => l.Id == (c.DefaultLocation ?? 0))).Name : "(none)"
            }).ToList();

            return View(viewModels);
        }

        // GET: Customer/Details/5
        public ActionResult Orders(int id, string sortOrder)
        {
            ViewData["TimeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "time_desc" : "";
            ViewData["OrderTotalSortParm"] = sortOrder == "OrderTotal" ? "order_total_desc" : "OrderTotal";

            IEnumerable<P1B.Customer> customers = CustomerRepo.GetAllCustomers();
            foreach (var customer in customers)
            {
                customer.FullName = customer.ReturnFullName();
            }
            IEnumerable<P1B.Location> locations = LocRepo.GetAllLocations();
            List<Project1.BLL.Cupcake> cupcakes = CupcakeRepo.GetAllCupcakes().OrderBy(c => c.Id).ToList();
            IEnumerable<P1B.Order> orders = CustomerRepo.GetCustomerOrderHistory(id).ToList();
            List<OrderViewModel> viewModels = new List<OrderViewModel>();

            ViewData["CustomerName"] = customers.Single(c => c.Id == id).ReturnFullName();
            ViewData["CustomerId"] = id;

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

        // GET: Customer/Create
        public ActionResult Create()
        {
            var viewModel = new CustomerViewModel
            {
                Locations = LocRepo.GetAllLocations().ToList()
            };

            // give the Create view values for its dropdown
            return View(viewModel);
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(P1B.Customer customer)
        {
            try
            {
                if (customer.FirstName.Length == 0 || customer.LastName.Length == 0)
                {
                    string message = "The customer must have a first and last name.";
                    TempData["ErrorMessage"] = message;
                    _logger.LogWarning(message);
                    return RedirectToAction("Error", "Home");
                }
                if (CustomerRepo.CheckCustomerFullNameExists(customer.ReturnFullName()))
                {
                    string message = "There is already a customer in the system with that first and last name.";
                    TempData["ErrorMessage"] = message;
                    _logger.LogWarning(message);
                    return RedirectToAction("Error", "Home");
                }
                int defLocation = customer.DefaultLocation ?? 0;
                if (defLocation != 0)
                {
                    bool LocationExists = LocRepo.CheckLocationExists(defLocation);
                    if (!LocationExists)
                    {
                        string message = "This location is not in the database.";
                        TempData["ErrorMessage"] = message;
                        _logger.LogWarning(message);
                        return RedirectToAction("Error", "Home");
                    }
                }

                // TODO: Add insert logic here
                var newCustomer = new P1B.Customer
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    DefaultLocation = customer.DefaultLocation ?? null
                };

                // TODO: Add insert logic here
                CustomerRepo.AddCustomer(newCustomer);
                return RedirectToAction(nameof(Index));
                // TODO: Add insert logic here
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Customer/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: Customer/Edit/5
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

        // GET: Customer/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Customer/Delete/5
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