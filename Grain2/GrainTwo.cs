using GrainInterfaces;
using Orleans;
using System.Threading.Tasks;

namespace Grains
{
    public class GrainTwo : Grain, IGrainTwo
    {
        public Task<string> SayHello()
        {
            return Task.FromResult("Hello from GrainTwo");
        }
    }
}
