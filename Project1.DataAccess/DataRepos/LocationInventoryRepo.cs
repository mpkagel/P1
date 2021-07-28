using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Project1.BLL.IDataRepos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using P1B = Project1.BLL;

namespace Project1.DataAccess.DataRepos
{
    public class LocationInventoryRepo : IProject1Repo, ILocationInventoryRepo
    {
        private readonly ILogger<LocationInventoryRepo> _logger;

        private static Project1Context Context { get; set; }

        public LocationInventoryRepo(Project1Context dbContext)
        {
            Context = dbContext;
        }

        public void SaveChangesAndCheckException()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex.ToString());
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
            }
        }

        public IEnumerable<P1B.LocationInventory>
            GetLocationInventoryByLocationId(int locationId)
        {
            try
            {
                return Mapper.Map(Context.LocationInventories.Where(li => li.LocationId == locationId));
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public void FillLocationInventory(int locationId)
        {
            try
            {
                foreach (var item in Context.Ingredients.ToList())
                {
                    var locationInv = new Project1.DataAccess.LocationInventory();
                    locationInv.IngredientId = item.IngredientId;
                    locationInv.LocationId = locationId;
                    locationInv.Amount = 120;
                    Context.LocationInventories.Add(locationInv);
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
            }

            SaveChangesAndCheckException();
        }

        public void UpdateLocationInv(int locationId, Dictionary<int, Dictionary<int, decimal>> recipes,
            List<Project1.BLL.OrderItem> orderItems)
        {
            // For each cupcake in the order, take that cupcake recipe and cupcake qnty, and subtract
            // the order ingredient amounts required from the store location's inventory.
            // The store location should already have been checked to make sure that its inventory
            // will not go negative from the order.

            try
            {
                foreach (var locationInv in Context.LocationInventories.Where(li => li.LocationId == locationId))
                {
                    foreach (var orderItem in orderItems)
                    {
                        locationInv.Amount -= recipes[orderItem.CupcakeId][locationInv.IngredientId] * (orderItem.Quantity ?? 0);
                    }
                }
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
            }
            SaveChangesAndCheckException();
        }
    }
}
