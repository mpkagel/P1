using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BLL.IDataRepos
{
    public interface IRecipeItemRepo
    {
        Dictionary<int, Dictionary<int, decimal>> GetRecipes(List<Project1.BLL.OrderItem> orderItems);
    }
}
