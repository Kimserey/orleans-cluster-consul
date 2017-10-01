using Library;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using System;
using System.IO;
using System.Net;

namespace OrleansSilo2
{
    // This project reference Grain2
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

            SiloFactory.InitializeSilo(deploymentId, port, proxy);

            Console.WriteLine("Silo running:");
            Console.WriteLine($"Cluster '{deploymentId}'");
            Console.WriteLine($"Silo port {port}'");
            Console.WriteLine($"Proxy gateway port '{proxy}'");
            Console.ReadLine();
        }
    }
}
