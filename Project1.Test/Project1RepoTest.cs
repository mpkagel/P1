using Microsoft.EntityFrameworkCore;
using Project1.BLL.IDataRepos;
using Project1.DataAccess;
using Project1.DataAccess.DataRepos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using P1B = Project1.BLL;

namespace Project1.Test
{
    public class Project1RepoTest
    {
        //[Fact]
        //public void TestCheckLocationExistsTrue()
        //{
            // Arrange
        //    public static void Main(string[] args)
        //    {
        //        CreateWebHostBuilder(args).Build().Run();
        //    }

        //    CreateWebHostBuilder(string[] args) =>
        //        WebHost.CreateDefaultBuilder(args)
        //            .UseStartup<Startup>()
        //            .UseNLog();

        //    var optionsBuilder = new DbContextOptionsBuilder<Project1Context>();
        //    optionsBuilder.UseSqlServer(Configuration.ConnectionString);
        //    var options = optionsBuilder.Options;

        //    using (var dbContext = new Project1Context(options))
        //    {
        //        ILocationRepo p0Repo = new LocationRepo(dbContext);
        //        var locations = p0Repo.GetAllLocations();
        //        // Act and Assert
        //        Assert.True(p0Repo.CheckLocationExists(locations.ToList().First().Id));
        //    }
        //}

        //[Fact]
        //public void TestCheckLocationExistsFalse()
        //{
        //    // Arrange
        //    var optionsBuilder = new DbContextOptionsBuilder<Project1Context>();
        //    optionsBuilder.UseSqlServer(SecretConfiguration.ConnectionString);
        //    var options = optionsBuilder.Options;

        //    using (var dbContext = new Project1Context(options))
        //    {
        //        ILocationRepo p0Repo = new LocationRepo(dbContext);

        //        // Act and Assert
        //        Assert.False(p0Repo.CheckLocationExists(100000));
        //    }
        //}

        //[Fact]
        //public void TestCheckCustomerExistsTrue()
        //{
        //    // Arrange
        //    var optionsBuilder = new DbContextOptionsBuilder<Project1Context>();
        //    optionsBuilder.UseSqlServer(SecretConfiguration.ConnectionString);
        //    var options = optionsBuilder.Options;

        //    using (var dbContext = new Project1Context(options))
        //    {
        //        ICustomerRepo p0Repo = new CustomerRepo(dbContext);

        //        var customers = p0Repo.GetAllCustomers();

        //        // Act and Assert
        //        Assert.True(p0Repo.CheckCustomerExists(customers.ToList().First().Id));
        //    }
        //}

        //[Fact]
        //public void TestCheckCustomerExistsFalse()
        //{
        //    // Arrange
        //    var optionsBuilder = new DbContextOptionsBuilder<Project1Context>();
        //    optionsBuilder.UseSqlServer(SecretConfiguration.ConnectionString);
        //    var options = optionsBuilder.Options;

        //    using (var dbContext = new Project1Context(options))
        //    {
        //        ICustomerRepo p0Repo = new CustomerRepo(dbContext);

        //        // Act and Assert
        //        Assert.False(p0Repo.CheckCustomerExists(100000));
        //    }
        //}

        //[Fact]
        //public void TestCheckCupcakeExistsTrue()
        //{
        //    // Arrange
        //    var optionsBuilder = new DbContextOptionsBuilder<Project1Context>();
        //    optionsBuilder.UseSqlServer(SecretConfiguration.ConnectionString);
        //    var options = optionsBuilder.Options;

        //    using (var dbContext = new Project1Context(options))
        //    {
        //        ICupcakeRepo p0Repo = new CupcakeRepo(dbContext);
        //        var cupcakes = p0Repo.GetAllCupcakes();

        //        // Act and Assert
        //        Assert.True(p0Repo.CheckCupcakeExists(cupcakes.ToList().First().Id));
        //    }
        //}

        //[Fact]
        //public void TestCheckCupcakeExistsFalse()
        //{
        //    // Arrange
        //    var optionsBuilder = new DbContextOptionsBuilder<Project1Context>();
        //    optionsBuilder.UseSqlServer(SecretConfiguration.ConnectionString);
        //    var options = optionsBuilder.Options;

        //    using (var dbContext = new Project1Context(options))
        //    {
        //        ICupcakeRepo p0Repo = new CupcakeRepo(dbContext);

        //        // Act and Assert
        //        Assert.False(p0Repo.CheckCupcakeExists(100000));
        //    }
        //}
    }
}
