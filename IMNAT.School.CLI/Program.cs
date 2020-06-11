using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;



namespace IMNAT.School.CLI
{
    public class StartUp
    {
        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }
      

    }

    class Program
    {

        public class SchoolManagement
        {
            readonly SchoolDbContext _SchoolDbContext;

            public SchoolManagement(SchoolDbContext SchoolDbContext) => _SchoolDbContext = SchoolDbContext;

            public void Run()
            {
                Console.WriteLine("Test de connexion avec la BD");          
            }
        }
        public class MySettings
        {
            public string Connection { get; set; }
        }

        private static IServiceProvider CreateServiceProvider()
        {
            // create service collection
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            // create service provider
            return services.BuildServiceProvider();
        }


        public static void ConfigureServices(IServiceCollection Services)
        {
            var config = new ConfigurationBuilder()
                 //.SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json")
                 .Build();
            var appConfig = config.GetSection("application").Get<MySettings>();

            //var Services = new ServiceCollection();

            Services.AddDbContext<SchoolDbContext>(options => options.UseSqlServer(appConfig.Connection));
            Services.AddSingleton<SchoolManagement>();

        }

        static void Main(string[] args)
        {
            using (var scope = CreateServiceProvider().CreateScope())
            {
                scope.ServiceProvider.GetService<SchoolManagement>().Run();
            }

        }

        private class Factory : IDesignTimeDbContextFactory<SchoolDbContext>
        {
            public SchoolDbContext CreateDbContext(string[] args)
                => CreateServiceProvider().CreateScope().ServiceProvider.GetService<SchoolDbContext>();
        }

    }


}
            
    
