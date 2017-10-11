using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainInterface
{
    public interface IBank: IGrainWithStringKey
    {
        Task SetName(string name);
        Task AddCustomer(ICustomer customer);
        Task<List<ICustomer>> GetCustomers();
    }
}
