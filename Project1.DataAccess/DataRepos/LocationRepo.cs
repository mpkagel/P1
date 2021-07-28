using Microsoft.Extensions.Logging;
using Project1.BLL;
using Project1.BLL.IDataRepos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using P1B = Project1.BLL;

namespace Project1.DataAccess.DataRepos
{
    public class LocationRepo : IProject1Repo, ILocationRepo
    {
        private readonly ILogger<LocationRepo> _logger;

        private static Project1Context Context { get; set; }

        public LocationRepo(Project1Context dbContext)
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

        public bool CheckLocationExists(int locationId)
        {
            try
            {
                return Context.Locations.Any(l => l.LocationId == locationId);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public bool CheckLocationNameExists(string locationName)
        {
            try
            {
                return Context.Locations.Any(l => l.Name == locationName);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public void AddLocation(Project1.BLL.Location location)
        {
            var newLocation = Mapper.Map(location);
            Context.Locations.Add(newLocation);
            SaveChangesAndCheckException();
        }

        public P1B.Location GetLocationById(int id)
        {
            try
            {
                return Mapper.Map(Context.Locations.Single(l => l.LocationId == id));
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }

        }

        public int GetLastLocationAdded()
        {
            try
            {
                return Context.Locations.OrderByDescending(x => x.LocationId).First().LocationId;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return -1;
            }
        }

        public int? GetDefaultLocation(int customerId)
        {
            try
            {
                return Context.Customers.Single(c => c.CustomerId == customerId).DefaultLocation;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return -1;
            }
        }

        public IEnumerable<Project1.BLL.Location> GetAllLocations()
        {
            try
            {
                return Mapper.Map(Context.Locations.ToList());
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public IEnumerable<P1B.Order> GetLocationOrderHistory(int locationId)
        {
            try
            {
                return Mapper.Map(Context.CupcakeOrders.Where(co => co.LocationId == locationId).ToList());
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
