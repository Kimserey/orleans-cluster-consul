using GrainInterfaces;
using Orleans;
using System.Threading.Tasks;

namespace Grains
{
    public class GrainOne : Grain, IGrainOne
    {
        public Task<string> SayHello()
        {
            return Task.FromResult("Hello from GrainOne");
        }
    }
}