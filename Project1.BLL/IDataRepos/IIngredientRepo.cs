using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BLL.IDataRepos
{
    public interface IIngredientRepo
    {
        IEnumerable<Project1.BLL.Ingredient> GetIngredients();
    }
}
