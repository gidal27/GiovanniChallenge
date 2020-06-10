using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using IMNAT.School.Repositories.DAL;

namespace IMNAT.School.CLI
{
    
    public class ConfigurationFile
    {
        private static IServiceProvider _serviceProvider;
        public static void RegisterServices() {

            var services = new ServiceCollection();

            services.AddDbContext<SchoolDbContext>(options => options.UseSqlServer(Configuration["Data:BmesApi:ConnectionString"]));

            _serviceProvider = services.BuildServiceProvider(true);

        }

        public static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }


    }
}
