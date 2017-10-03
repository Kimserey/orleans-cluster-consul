using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace OrleansHostApp3.SQLMembership.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var port = args.Length > 0 ? args[0] : "20080";
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls($"http://localhost:{port}")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}