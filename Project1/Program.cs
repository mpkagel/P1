using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Project1
{
    public class Program
    {
        /// <remarks>
        ///  The following command is the command in Package Manager Console to scaffold and re-scaffold the
        ///  database. I have had to do this 4-5 times already, so I have the command at the top of the
        ///  program so I can grab it.
        /// </remarks>
        // Scaffold-DbContext "<your-connection-string>" 
        //      Microsoft.EntityFrameworkCore.SqlServer
        //      -Project <name-of-data-project>

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseNLog();

    }
}
