using Library;
using Orleans;
using System.Threading.Tasks;

namespace OrleansHostApp2.Grains
{
    public class GrainOne : Grain, IGrainOne
    {
        public Task<string> SayHello()
        {
            return Task.FromResult($"Hello from {DeploymentConstants.ONE}");
        }
    }
}