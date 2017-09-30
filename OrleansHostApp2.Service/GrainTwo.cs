using Library;
using Orleans;
using System.Threading.Tasks;

namespace OrleansHostApp2
{
    public class GrainTwo : Grain, IGrainTwo
    {
        public Task<string> SayHello()
        {
            return Task.FromResult("Hello from OrleansHostApp2.");
        }
    }
}
