using Library;
using Orleans.Runtime;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using Orleans.Storage;
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

            var silo = new SiloHost(Dns.GetHostName());
            silo.InitializeOrleansSilo();

            silo.Config.Globals.RegisterStorageProvider<AdoNetStorageProvider>("sql-store", new Dictionary<string, string>{
                ["AdoInvariant"] = "System.Data.SqlClient",
                ["DataConnectionString"] = "Data Source=.\\SQLExpress; Database=OrleansStore; Trusted_Connection=True;",
                ["UseJsonFormat"] = "true"
            });

            silo.Config.Globals.RegisterArangoStorageProvider("store", 
                password: "123456", 
                databaseName: "OrleansStore", 
                collectionName: "Main");

            if (!silo.StartOrleansSilo())
            {
                throw new Exception("Failed to start silo.");
            }

            Console.WriteLine("Silo started");
            Console.ReadLine();
        }
    }
}
