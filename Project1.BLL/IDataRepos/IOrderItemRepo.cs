using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BLL.IDataRepos
{
    public interface IOrderItemRepo
    {
        void AddCupcakeOrderItems(List<Project1.BLL.OrderItem> orderItems);
        IEnumerable<Project1.BLL.OrderItem> GetAllOrderItems();
        IEnumerable<Project1.BLL.OrderItem> GetOrderItems(int orderId);
    }
}
