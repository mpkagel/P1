using System;
using System.Collections.Generic;

#nullable disable

namespace Project1.DataAccess
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            LocationInventories = new HashSet<LocationInventory>();
            RecipeItems = new HashSet<RecipeItem>();
        }

        public int IngredientId { get; set; }
        public string Type { get; set; }
        public string Units { get; set; }

        public virtual ICollection<LocationInventory> LocationInventories { get; set; }
        public virtual ICollection<RecipeItem> RecipeItems { get; set; }
    }
}
