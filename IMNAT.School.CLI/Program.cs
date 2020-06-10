using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;



namespace IMNAT.School.CLI
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ConfigurationFile.RegisterServices();
            // IServiceScope scope = _serviceProvider.CreateScope();
            //scope.ServiceProvider.GetRequiredService<ConsoleApplication>().Run(); // A utiliser pour utiliser les services requi
            ConfigurationFile.DisposeServices();

            Console.WriteLine("Hello World!");
        }
       
    }
}