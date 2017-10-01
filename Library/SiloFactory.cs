using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using System;
using System.Net;

namespace Library
{
    public static class SiloFactory
    {
        public static void InitializeSilo(string deploymentId, int port, int proxy) {
            var options = new OrleansClusterOptions
            {
                Defaults = new NodeConfigurationOptions
                {
                    HostNameOrIPAddress = "localhost",
                    Port = port,
                    ProxyGatewayEndpoint = new IPEndPointOptions
                    {
                        Address = "localhost",
                        Port = proxy
                    }
                }
            };

            var config = new ClusterConfiguration();
            config.Globals.SetGlobalsForConsul(deploymentId);
            config.Defaults.SetDefaults(options.Defaults);
            config.Defaults.TraceToConsole = true;
            config.Defaults.DefaultTraceLevel = Orleans.Runtime.Severity.Info;

            var siloHost = new SiloHost($"{Dns.GetHostName()}-{port}", config);
            siloHost.InitializeOrleansSilo();

            var startedOk = siloHost.StartOrleansSilo();
            if (!startedOk)
            {
                throw new SystemException(String.Format("Failed to start Orleans silo '{0}' as a {1} node", siloHost.Name, siloHost.Type));
            }
        }
    }
}
