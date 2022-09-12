using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ConsoleApp.Controllers
{
    public class DataController
    {
        private readonly IDataService _dataService;
        private readonly ILogger<DataController> _logger;

        public DataController(IDataService dataService, ILoggerFactory loggerFactory)
        {
            _dataService = dataService;
            _logger = loggerFactory.CreateLogger<DataController>();
        }

        public async Task PrintAsync(string file)
        {
            try
            {
                await _dataService.PrintAsync(file);

                _logger.LogInformation("Printing complete");
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
            }
        }
    }
}