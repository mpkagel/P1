using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BLL.IDataRepos
{
    public interface ILocationRepo
    {
        bool CheckLocationExists(int locationId);
        bool CheckLocationNameExists(string locationName);
        void AddLocation(Project1.BLL.Location location);
        Location GetLocationById(int locationId);
        int GetLastLocationAdded();
        int? GetDefaultLocation(int customerId);
        IEnumerable<Project1.BLL.Location> GetAllLocations();
        IEnumerable<Project1.BLL.Order> GetLocationOrderHistory(int locationId);
    }
}
