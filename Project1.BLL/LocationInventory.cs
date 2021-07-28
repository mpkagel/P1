using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BLL
{
    public class LocationInventory
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public int IngredientId { get; set; }
        public decimal Amount { get; set; }
    }
}
