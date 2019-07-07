using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LGDShop.DataAccess;
using LGDShop.DataAccess.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace LGDShop.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //default outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
            //var outputTemplate = "[{Timestamp:HH:mm:ss}] [{Level:u3}] [{SourceContext}]{NewLine}{Message:lj}{NewLine}in method {MemberName} at {FilePath}:{LineNumber}{NewLine}{Exception}{NewLine}";
            var outputTemplate = "[{Timestamp:HH:mm:ss}] [{Level:u3}] [{SourceContext}] [{MemberName}] [{FilePath}:{LineNumber}]{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}";
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt",
                outputTemplate: outputTemplate,
                rollingInterval: RollingInterval.Day,
                fileSizeLimitBytes: 107374182,   //100MB
                rollOnFileSizeLimit: true)
                .CreateLogger();         

            try
            {
                Log.Information("Starting web host");

                var host = CreateWebHostBuilder(args).Build();

                //seed database (also auto-update pending migrations if there is any)
                SeedShopDb(host);

                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog();

        private static void SeedShopDb(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var shopDbContext = services.GetRequiredService<ShopDbContext>();
                    ApiDbSeeder.Seed(shopDbContext).Wait();
                    var applicationDbContext = services.GetRequiredService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    ApplicationDbSeeder.Seed(applicationDbContext, roleManager, userManager).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }
        }
    }
}
