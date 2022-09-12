using ConsoleApp.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class DataProcessor : IDataProcessor
    {
        private readonly IDataFileHandler _dataFileHandler;
        private readonly ILogger<DataProcessor> _logger;

        public DataProcessor(IDataFileHandler dataFileHandler, ILoggerFactory logger)
        {
            _dataFileHandler = dataFileHandler;
            _logger = logger.CreateLogger<DataProcessor>();
        }

        public async Task<IEnumerable<ImportedObject>> GetDataAsync(string file)
        {
            var data = await _dataFileHandler.GetImportedObjectsAsync(file);
            try
            {
                return data.AssignChildrenCount();
            }
            catch (Exception ex)
            {
                _logger.LogError("AssignChildrenCount failed!",ex.Message);
                throw;
            }
        }
    }
}