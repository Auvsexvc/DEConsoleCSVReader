using System.Threading.Tasks;

namespace ConsoleApp
{
    public interface IDataService
    {
        Task PrintAsync(string file);
    }
}