using ConsoleApp.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Program
    {
        private static async Task Main()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging(option => option.AddConsole())
                .AddSingleton<IDataFileHandler, DataFileHandler>()
                .AddSingleton<IDataProcessor, DataProcessor>()
                .AddSingleton<IDataService, DataService>()
                .AddSingleton<DataController>()
                .BuildServiceProvider();

            var logger = serviceProvider.GetService<ILoggerFactory>()
            .CreateLogger<Program>();

            logger.LogDebug("Starting application");

            var reader = serviceProvider.GetService<DataController>();
            await reader.PrintAsync("data.csv");

            Console.ReadLine();
        }
    }
}