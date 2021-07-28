using System;
using System.Collections.Generic;

#nullable disable

namespace Project1.DataAccess
{
    public partial class Cupcake
    {
        public Cupcake()
        {
            CupcakeOrderItems = new HashSet<CupcakeOrderItem>();
            RecipeItems = new HashSet<RecipeItem>();
        }

        public int CupcakeId { get; set; }
        public string Type { get; set; }
        public decimal Cost { get; set; }

        public virtual ICollection<CupcakeOrderItem> CupcakeOrderItems { get; set; }
        public virtual ICollection<RecipeItem> RecipeItems { get; set; }
    }
}
