using Microsoft.Extensions.Options;
using Orleans;
using Orleans.Runtime.Configuration;
using System;
using System.Threading.Tasks;

namespace Library
{
    public class GrainFactoryResolver : IGrainFactoryResolver
    {
        public IGrainFactory Get(string deploymentId)
        {
            GrainClient.Initialize(new ClientConfiguration
            {
                DeploymentId = deploymentId,
                GatewayProvider = ClientConfiguration.GatewayProviderType.Custom,
                DataConnectionString = "http://localhost:8500",
                CustomGatewayProviderAssemblyName = "OrleansConsulUtils",
                DefaultTraceLevel = Orleans.Runtime.Severity.Info
            });

            return GrainClient.GrainFactory;
        }

    }
}
