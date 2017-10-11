using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrainInterface
{
    public interface ICard: IGrainWithStringKey
    {
        Task Activate();
        Task<bool> IsActivated();
        Task SetName(string name);
        Task SetExpiry(DateTime date);
    }
}
