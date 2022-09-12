using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public interface IDataFileHandler
    {
        Task<IEnumerable<ImportedObject>> GetImportedObjectsAsync(string file);
    }
}