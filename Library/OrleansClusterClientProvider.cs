using Microsoft.Extensions.Options;
using Orleans;
using Orleans.Runtime.Configuration;
using System;
using System.Threading.Tasks;

namespace Library
{
    public class OrleansClusterClientProvider : IOrleansClusterClientProvider
    {
        public async Task<IClusterClient> Get(string deploymentId)
        {
            var config = CreateConsulClientConfiguration(deploymentId);
            var client = new ClientBuilder().UseConfiguration(config).Build();
            await client.Connect();
            return client;
        }

        public ClientConfiguration CreateConsulClientConfiguration(string deploymentId)
        {
            var clientConfig = new ClientConfiguration();
            clientConfig.DeploymentId = deploymentId;
            clientConfig.GatewayProvider = ClientConfiguration.GatewayProviderType.Custom;
            clientConfig.DataConnectionString = "http://localhost:8500";
            clientConfig.CustomGatewayProviderAssemblyName = "OrleansConsulUtils";
            clientConfig.TraceToConsole = true;
            return clientConfig;
        }

    }
}
