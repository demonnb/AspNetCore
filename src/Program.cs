using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AspCoreMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(ConfigConfiguration)
                .UseStartup<Startup>();

        static void ConfigConfiguration(WebHostBuilderContext webHostBuilderContext, IConfigurationBuilder configurationBuilder)
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
            }

    }
}
