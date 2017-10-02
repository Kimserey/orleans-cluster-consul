using Microsoft.Extensions.Options;
using Orleans;
using Orleans.Runtime;
using Orleans.Runtime.Configuration;
using System;
using System.Threading.Tasks;

namespace Library
{
    public class GrainFactoryResolver : IGrainFactoryResolver
    {
        public IGrainFactory Get(string deploymentId,
            ClientConfiguration.GatewayProviderType gatewayProvider = ClientConfiguration.GatewayProviderType.Custom,
            string dataConnectionString = "http://localhost:8500",
            string customGatewayProviderAssemblyName = "OrleansConsulUtils",
            Severity defaultTraceLevel = Severity.Verbose)
        {
            var client = new ClientBuilder()
                .UseConfiguration(
                    new ClientConfiguration
                    {
                        DeploymentId = deploymentId,
                        GatewayProvider = gatewayProvider,
                        DataConnectionString = dataConnectionString,
                        CustomGatewayProviderAssemblyName = customGatewayProviderAssemblyName,
                        DefaultTraceLevel = defaultTraceLevel
                    })
                .Build();

            if (!client.IsInitialized)
            {
                client.Connect().Wait();
            }

            return client;
        }

    }
}

