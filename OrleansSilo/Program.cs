using Library;
using Orleans.Runtime.Configuration;
using Orleans.StorageProvider.Arango;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OrleansSilo
{
    class Program
    {
        static void Main(string[] args)
        {
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

            SiloFactory.InitializeSilo(deploymentId, port, proxy, clusterConfig => {
                clusterConfig.Globals.SeedNodes.Add(new IPEndPoint(IPAddress.Loopback, 30023));
                clusterConfig.Globals.LivenessType = GlobalConfiguration.LivenessProviderType.MembershipTableGrain;
                clusterConfig.Globals.RegisterArangoStorageProvider("store", password: "123456", collectionName: "Orleans_store");

                if (port == 30023)
                {
                    clusterConfig.PrimaryNode = new IPEndPoint(IPAddress.Loopback, port);
                }
            });

            Console.WriteLine("Silo running:");
            Console.WriteLine($"Deployment: '{deploymentId}'");
            Console.WriteLine($"Port: {port}'");
            Console.WriteLine($"Gateway: '{proxy}'");
            Console.ReadLine();
        }
    }
}
