using Microsoft.Extensions.Logging;
using P1B = Project1.BLL;
using Project1.BLL.IDataRepos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Project1.DataAccess.DataRepos
{
    public class CupcakeRepo : IProject1Repo, ICupcakeRepo
    {
        private readonly ILogger<CupcakeRepo> _logger;

        private static Project1Context Context { get; set; }

        public CupcakeRepo(Project1Context dbContext)
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

        public bool CheckCupcakeExists(int cupcakeId)
        {
            try
            {
                return Context.Cupcakes.Any(l => l.CupcakeId == cupcakeId);
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }
        }

        public P1B.Cupcake GetCupcake(int cupcakeId)
        {
            try
            {
                return Mapper.Map(Context.Cupcakes.Single(c => c.CupcakeId == cupcakeId));
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }

        public IEnumerable<P1B.Cupcake> GetAllCupcakes()
        {
            try
            {
                return Mapper.Map(Context.Cupcakes.ToList());
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.ToString());
                return null;
            }
        }
    }
}
