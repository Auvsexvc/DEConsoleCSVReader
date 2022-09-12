using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public interface IDataProcessor
    {
        Task<IEnumerable<ImportedObject>> GetDataAsync(string file);
    }
}