using Db_Context;
using IMNAT.School.Services.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel;
using Microsoft.EntityFrameworkCore.Design;
using System.Data;
using System.Linq;

namespace IMNAT.School.Services.Services.Configurations
{
    public static class StartUp
    {
        public class MySettings
        {
            public string Connection { get; set; }
            public string[] InitialCourses { get; set; }
        }

        public static IServiceProvider CreateServiceProvider()
        {
            // create service collection
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            // create service provider
            return services.BuildServiceProvider();
        }

        public static class FileConfigRead
        {

            public static string[] InitialContent { get; set; }
        }


        public static void ConfigureServices(IServiceCollection Services)
        {

            var config = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json")
                  .Build();

            var appConfig = config.GetSection("application").Get<MySettings>(); // get connection strings
            var InitialData = config.GetSection("Data").Get<MySettings>(); //to get initial data to seed database

            FileConfigRead.InitialContent = InitialData.InitialCourses;
            var InitialContent = appConfig.InitialCourses;

            Services.AddDbContext<SchoolDbContext>(options => options.UseSqlServer(appConfig.Connection, b => b.MigrationsAssembly("IMNAT.School.Models")));
            Services.AddSingleton<SchoolManagement>();
        }

        private class Factory : IDesignTimeDbContextFactory<SchoolDbContext>
        {
            public SchoolDbContext CreateDbContext(string[] args)
                => CreateServiceProvider().CreateScope().ServiceProvider.GetService<SchoolDbContext>();
        }

    }
}
