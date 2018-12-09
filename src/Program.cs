using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using AspCoreMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace AspCoreMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();
            var host = CreateWebHostBuilder(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<MovieContext>();
                    // requires using Microsoft.EntityFrameworkCore;
                    context.Database.Migrate();
                    // Requires using RazorPagesMovie.Models;
                    SeedData.Initialize(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }

            host.Run();
        }

        public static IWebHost CreateWebHostBuilder(string[] args) =>
            WebHost
            .CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, configurationBuilder) =>
            {
                configurationBuilder.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("azureKeyVault.json", false, true)
                    .AddEnvironmentVariables();
                
                var config = configurationBuilder.Build();
                
                configurationBuilder.AddAzureKeyVault(
                    $"https://{config["vault"]}.vault.azure.net/",
                    config["clientId"],
                    config["clientSecret"]
                );
            })
            .UseStartup<Startup>()
            .Build();
    }
}
