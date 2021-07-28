using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.ViewModels
{
    public class OrderViewModel
    {
        [BindNever]
        public int OrderId { get; set; }

        [Display(Name = "Location Name")]
        [Required]
        public int LocationId { get; set; }

        [Display (Name = "Location Name")]
        public string LocationName { get; set; }

        [Display(Name = "Customer Name")]
        [Required]
        public int CustomerId { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Order Time")]
        public DateTime OrderTime { get; set; }

        public List<Project1.BLL.Location> Locations { get; set; } = null;
        public List<Project1.BLL.Customer> Customers { get; set; } = null;

        public List<Project1.BLL.Cupcake> Cupcakes { get; set; } = null;

        [Display(Name = "What Kinds of Cupcakes?")]
        public List<Project1.BLL.OrderItem> OrderItems { get; set; } = null;

        public decimal OrderTotal { get; set; }



    }
}
