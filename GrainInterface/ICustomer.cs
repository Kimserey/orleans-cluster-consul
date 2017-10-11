using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainInterface
{
    public interface ICustomer: IGrainWithStringKey
    {
        Task SetName(string name);
        Task<string> GetName();
    }
}
