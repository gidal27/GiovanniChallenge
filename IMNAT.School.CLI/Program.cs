using Db_Context;
//using IMNAT.School.Models.Entities.DomainModels;
using IMNAT.School.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

            public void SeedDatabase()
            {
                // we feed database with first elements 
                var table = _SchoolDbContext.Courses;
                string[] InitialValues = FileConfigRead.InitialContent; // a surveiller durant le temps d'execution
                Course InitialSingleContent = new Course();
                List<Course> InitialContent = new List<Course>();
             
                if (table.CountAsync().Result == 0) // we check if there  is already 
                {
                    for (int i = 0; i < InitialValues.Length; i++)
                    {
                        InitialSingleContent.Name = InitialValues[i];

                        InitialContent.Add(InitialSingleContent);
                    };

                    try
                    {
                        foreach (Course course in InitialContent)
                        {
                            table.AddAsync(course);

                        }
                        _SchoolDbContext.SaveChangesAsync();
                    }
                    catch (Exception e) { Console.WriteLine(e.ToString()); }

                }
            }
                    
                    public void Run()
                    {
                        Console.WriteLine("Test de creation de la BD");
                    }
                
            
        }
        public class MySettings
        {
            public string Connection { get; set; }
            public string[] InitialCourses { get; set; }
        }

        private static IServiceProvider CreateServiceProvider()
        {
            // create service collection
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            // create service provider
            return services.BuildServiceProvider();
        }

        public static class FileConfigRead {

            public static  string[] InitialContent { get; set; }
        }


        public static void ConfigureServices(IServiceCollection Services)
        {

            var config = new ConfigurationBuilder()
                  //.SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json")
                  .Build();

            var appConfig = config.GetSection("application").Get<MySettings>(); // get connection strings
            var InitialData = config.GetSection("Data").Get<MySettings>(); //to get initial data to seed database

            FileConfigRead.InitialContent = InitialData.InitialCourses;
            var InitialContent = appConfig.InitialCourses;

            Services.AddDbContext<SchoolDbContext>(options => options.UseSqlServer(appConfig.Connection, b => b.MigrationsAssembly("IMNAT.School.Models")));
            Services.AddSingleton<SchoolManagement>();

        
        }

        static void Main(string[] args)
        {
            // Exe
            using (var scope = CreateServiceProvider().CreateScope())
            {

                scope.ServiceProvider.GetService<SchoolManagement>().SeedDatabase();
            }
        }

        private class Factory : IDesignTimeDbContextFactory<SchoolDbContext>
        {
            public SchoolDbContext CreateDbContext(string[] args)
                => CreateServiceProvider().CreateScope().ServiceProvider.GetService<SchoolDbContext>();
        }

    }


}
            
    
