using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace OrleansHostApp4.ZKMembership.Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls("http://localhost:20033")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
