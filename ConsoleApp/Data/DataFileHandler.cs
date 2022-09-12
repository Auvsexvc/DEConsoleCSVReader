using ConsoleApp.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class DataFileHandler : IDataFileHandler
    {
        private readonly ILogger<DataFileHandler> _logger;

        public DataFileHandler(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger<DataFileHandler>();
        }

        public async Task<IEnumerable<ImportedObject>> GetImportedObjectsAsync(string file)
        {
            var lines = await GetLinesFromFileAsync(file);

            try
            {
                return lines.Where(line => line.ToImportedObject() != null).Select(line => line.ToImportedObject().MakeUp());
            }
            catch (Exception ex)
            {
                _logger.LogError("GetImportedObjects failed!", ex.Message);
                throw;
            }
        }

        private async Task<IEnumerable<string>> GetLinesFromFileAsync(string fileToImport)
        {
            List<string> retList = new List<string>();

            using (var streamReader = new StreamReader(fileToImport))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = await streamReader.ReadLineAsync();
                    if (!string.IsNullOrEmpty(line))
                    {
                        retList.Add(line);
                    }
                }
            }
            return retList;
        }
    }
}