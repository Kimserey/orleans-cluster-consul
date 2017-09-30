using Orleans;
using System.Threading.Tasks;

namespace GrainInterfaces
{
    public interface IGrainTwo : IGrainWithGuidKey
    {
        Task<string> SayHello();
    }
}
