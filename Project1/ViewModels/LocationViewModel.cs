using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.ViewModels
{
    public class LocationViewModel
    {
        [Display(Name = "Location Name")]
        [Required]
        [MaxLength(100)]
        [DataType(DataType.MultilineText)]
        public string LocationName { get; set; }
    }
}
