using Microsoft.Extensions.Logging;
using Project1.BLL.IDataRepos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Project1.DataAccess.DataRepos
{
    public class RecipeItemRepo : IProject1Repo, IRecipeItemRepo
    {
        private readonly ILogger<RecipeItemRepo> _logger;

        private static Project1Context Context { get; set; }

        public RecipeItemRepo(Project1Context dbContext)
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

        public Dictionary<int, Dictionary<int, decimal>> GetRecipes(List<Project1.BLL.OrderItem> orderItems)
        {
            try
            {
                Dictionary<int, Dictionary<int, decimal>> recipes = new Dictionary<int, Dictionary<int, decimal>>();

                // Get each recipe for each cupcake that is in the order
                foreach (var orderItem in orderItems)
                {
                    Dictionary<int, decimal> recipe = new Dictionary<int, decimal>();
                    foreach (var recipeItem in Context.RecipeItems.Where(r => r.CupcakeId == orderItem.CupcakeId).ToList())
                    {
                        recipe[recipeItem.IngredientId] = recipeItem.Amount;
                    }
                    recipes[orderItem.CupcakeId] = recipe;
                }

                return recipes;
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
