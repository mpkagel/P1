using Microsoft.AspNetCore.Mvc.ModelBinding;
using Project1.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project1.ViewModels
{
    public class LocationInventoryViewModel
    {
        [BindNever]
        public int LocationId { get; set; }

        public string LocationName { get; set; }

        [BindNever]
        public int IngredientId { get; set; }

        [Display (Name = "Ingredient Type")]
        public string IngredientType { get; set; }

        [Display(Name = "Ingredient Units")]
        public string IngredientUnits { get; set; }

        [Display(Name = "Ingredient Amount")]
        public decimal IngredientAmount { get; set; }

        public List<Ingredient> Ingredients { get; set; }


        //[Required]
        //[MaxLength(30)]
        //public string Title { get; set; }

        //[Display(Name = "Release Date")]
        //[DataType(DataType.Date)]
        //[Required] // now default value (1/1/1 won't be accepted)
        //public DateTime ReleaseDate { get; set; }

        //public Genre Genre { get; set; }

        //public List<Genre> Genres { get; set; }

        //// here we can provide other info the view may need
        ////public string LoggedInUser { get; set; }

    }
}
