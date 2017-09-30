using Orleans;
using System.Threading.Tasks;

namespace Library
{
    public interface IGrainTwo : IGrainWithGuidKey
    {
        Task<string> SayHello();
    }
}
