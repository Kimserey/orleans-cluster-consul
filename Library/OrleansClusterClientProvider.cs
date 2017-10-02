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
            var client = new ClientBuilder()
                .UseConfiguration(
                    new ClientConfiguration
                    {
                        DeploymentId = deploymentId,
                        GatewayProvider = ClientConfiguration.GatewayProviderType.Custom,
                        DataConnectionString = "http://localhost:8500",
                        CustomGatewayProviderAssemblyName = "OrleansConsulUtils",
                        DefaultTraceLevel = Orleans.Runtime.Severity.Verbose
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

