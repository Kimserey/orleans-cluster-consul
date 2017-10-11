using GrainInterface;
using Orleans;
using Orleans.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainWithState
{
    [StorageProvider(ProviderName = "sql-store")]
    public class Customer : Grain<CustomerState>, ICustomer
    {
        public Task<string> GetName()
        {
            return Task.FromResult(State.Name);
        }

        public Task SetName(string name)
        {
            State.Name = name;
            return WriteStateAsync();
        }
    }

    public class CustomerState
    {
        public string Name { get; set; }
    }
}
