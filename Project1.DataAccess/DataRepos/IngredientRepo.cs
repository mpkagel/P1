using Microsoft.Extensions.Logging;
using P1B = Project1.BLL;
using Project1.BLL.IDataRepos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Project1.DataAccess.DataRepos
{
    public class IngredientRepo : IProject1Repo, IIngredientRepo
    {
        private readonly ILogger<IngredientRepo> _logger;

        private static Project1Context Context { get; set; }

        public IngredientRepo(Project1Context dbContext)
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

        public IEnumerable<P1B.Ingredient> GetIngredients()
        {
            return Mapper.Map(Context.Ingredients);
        }
    }
}
