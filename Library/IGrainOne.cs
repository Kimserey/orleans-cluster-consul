using Orleans;
using System.Threading.Tasks;

namespace Library
{
    public interface IGrainOne : IGrainWithGuidKey
    {
        Task<string> SayHello();
    }
}
