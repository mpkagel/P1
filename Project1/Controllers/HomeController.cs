using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project1.BLL.IDataRepos;
using Project1.Models;
using MoreLinq;

namespace Project1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ICustomerRepo customerRepo, ILocationRepo locationRepo,
            IOrderRepo orderRepo, ICupcakeRepo cupcakeRepo, IOrderItemRepo orderItemRepo,
            ILocationInventoryRepo locationInventoryRepo, IRecipeItemRepo recipeItemRepo,
            ILogger<HomeController> logger)
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

        public IActionResult Index()
        {
            Project1.ViewModels.HomeViewModel viewModel = new ViewModels.HomeViewModel
            {
                Locations = LocRepo.GetAllLocations().ToList(),
                Customers = CustomerRepo.GetAllCustomers().ToList(),
                Cupcakes = CupcakeRepo.GetAllCupcakes().ToList(),
                Orders = OrderRepo.GetAllOrders().ToList(),
                OrderItems = OrderItemRepo.GetAllOrderItems().ToList()

            };
            decimal sum = 0;
            decimal incrementer = 0;

            foreach (var order in viewModel.Orders)
            {
                sum += order.GetTotalCost(OrderItemRepo.GetOrderItems(order.Id).ToList(), viewModel.Cupcakes);
                incrementer++;
            }
            if (incrementer > 0)
            {
                viewModel.OrderTotalAverage = sum / incrementer;
            }

            if (viewModel.Locations.Count > 0)
            {
                viewModel.LocationMostOrders = viewModel.Locations.MaxBy(sL =>
                LocRepo.GetLocationOrderHistory(sL.Id).Count()).OrderBy(sL => sL.Id).First().Name;
                if (viewModel.Orders.Count > 0)
                {
                    viewModel.LocationWithLatestOrder = viewModel.Locations.Single(l => l.Id ==
                        viewModel.Orders.MaxBy(o => o.OrderTime).First().OrderLocation).Name;
                }
            }

            return View(viewModel);
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            if (TempData.ContainsKey("ErrorMessage"))
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
    }
}
