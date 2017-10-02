using Orleans;
using Orleans.Runtime;
using Orleans.Runtime.Configuration;
using System.Threading.Tasks;

namespace Library
{
    public interface IGrainFactoryResolver
    {
        IGrainFactory Get(string deploymentId,
            ClientConfiguration.GatewayProviderType gatewayProvider = ClientConfiguration.GatewayProviderType.Custom,
            string dataConnectionString = "http://localhost:8500",
            string customGatewayProviderAssemblyName = "OrleansConsulUtils",
            Severity defaultTraceLevel = Severity.Verbose);
    }
}
