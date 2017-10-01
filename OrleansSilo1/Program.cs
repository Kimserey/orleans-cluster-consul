using Library;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OrleansSilo1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Removes assembly loading warninga, default folder scanned for grains.
            Directory.CreateDirectory("Applications");

            Console.WriteLine($"Running with arguments '{string.Join("/", args)}'");

            var deploymentId = "main";
            var port = 30023;
            var proxy = 40023;
            
            if (args.Length >= 3)
            {
                deploymentId = args[0];
                port = Int32.Parse(args[1]);
                proxy = Int32.Parse(args[2]);
            }

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

            Console.WriteLine("Silo running:");
            Console.WriteLine($"Cluster '{deploymentId}'");
            Console.WriteLine($"Silo address '{options.Defaults.HostNameOrIPAddress}:{options.Defaults.Port}'");
            Console.WriteLine($"Proxy gateway '{options.Defaults.ProxyGatewayEndpoint.Address}:{options.Defaults.ProxyGatewayEndpoint.Port}'");
            Console.ReadLine();
        }
    }
}
