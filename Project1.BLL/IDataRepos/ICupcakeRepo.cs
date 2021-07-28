using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.BLL.IDataRepos
{
    public interface ICupcakeRepo
    {
        bool CheckCupcakeExists(int cupcakeId);
        Project1.BLL.Cupcake GetCupcake(int cupcakeId);
        IEnumerable<Project1.BLL.Cupcake> GetAllCupcakes();
    }
}
