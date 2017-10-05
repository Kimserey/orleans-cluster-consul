using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrainInterface
{
    public interface IBankAccountGrain: IGrainWithGuidKey
    {
        Task AddCard(string cardNumber);
        Task<IEnumerable<string>> GetCards(string cardNumber);
    }
}
