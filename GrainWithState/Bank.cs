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
    [StorageProvider(ProviderName = "store")]
    public class Bank : Grain<BankState>, IBank
    {
        public async Task AddCustomer(ICustomer customer)
        {
            if (State.Customers == null)
            {
                State.Customers = new List<ICustomer>();
                await WriteStateAsync();
            }

            if (!State.Customers.Any(c => c.GetPrimaryKeyString() == customer.GetPrimaryKeyString()))
            {
                State.Customers.Add(customer);
                await WriteStateAsync();
            }
        }

        public Task<List<ICustomer>> GetCustomers()
        {
            return Task.FromResult(State.Customers);
        }

        public Task SetName(string name)
        {
            State.Name = name;
            return WriteStateAsync();
        }
    }

    public class BankState
    {
        public List<ICustomer> Customers { get; set; }
        public string Name { get; set; }
    }
}
