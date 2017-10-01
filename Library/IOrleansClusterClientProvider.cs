using Orleans;
using System.Threading.Tasks;

namespace Library
{
    public interface IGrainFactoryResolver
    {
        IGrainFactory Get(string deploymentId);
    }
}
