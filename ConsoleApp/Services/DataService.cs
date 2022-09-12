using ConsoleApp.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class DataService : IDataService
    {
        private readonly ILogger<DataService> _logger;
        private readonly IDataProcessor _dataProcessor;

        public DataService(IDataProcessor dataProcessor, ILoggerFactory loggerFactory)
        {
            _dataProcessor = dataProcessor;
            _logger = loggerFactory.CreateLogger<DataService>();
        }

        public async Task PrintAsync(string file)
        {
            var importedObjects = await _dataProcessor.GetDataAsync(file);

            _logger.LogInformation($"Retrieving data from {file}\n");

            foreach (var database in importedObjects.Where(database => database.Type == nameof(ImportedObjectType.Database)))
            {
                Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");
                foreach (var table in importedObjects)
                {
                    if (table.ParentType == database.Type && table.ParentName == database.Name)
                    {
                        Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");

                        foreach (var column in importedObjects)
                        {
                            if (column.ParentType == table.Type && column.ParentName == table.Name)
                            {
                                Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");
                            }
                        }
                    }
                }
            }
        }
    }
}