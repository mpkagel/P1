using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.ViewModels
{
    public class HomeViewModel
    {
        public List<Project1.BLL.Location> Locations { get; set; } = null;
        public List<Project1.BLL.Customer> Customers { get; set; } = null;
        public List<Project1.BLL.Cupcake> Cupcakes { get; set; } = null;
        public List<Project1.BLL.Order> Orders { get; set; } = null;
        public List<Project1.BLL.OrderItem> OrderItems { get; set; } = null;

        [Display(Name = "Average Total All Orders")]
        public decimal OrderTotalAverage { get; set; } = 0;

        [Display(Name = "Location With Most Orders")]
        public string LocationMostOrders { get; set; } = "(none)";

        [Display(Name = "Location With Latest Order")]
        public string LocationWithLatestOrder { get; set; } = "(none)";
    }
}
