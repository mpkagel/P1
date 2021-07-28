using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BLL.IDataRepos
{
    public interface IOrderRepo
    {
        bool CheckOrderExists(int orderId);
        void AddCupcakeOrder(Project1.BLL.Order order);
        int GetLastCupcakeOrderAdded();
        Project1.BLL.Order GetOrder(int orderId);
        IEnumerable<Project1.BLL.Order> GetAllOrders();
    }
}
