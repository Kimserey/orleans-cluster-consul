using Orleans;
using System.Threading.Tasks;

namespace GrainInterfaces
{
    public interface IGrainOne : IGrainWithGuidKey
    {
        Task<string> SayHello();
    }
}
