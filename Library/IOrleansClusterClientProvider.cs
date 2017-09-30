using Orleans;
using System.Threading.Tasks;

namespace Library
{
    public interface IOrleansClusterClientProvider
    {
        Task<IClusterClient> Get(string providerName);
    }
}
