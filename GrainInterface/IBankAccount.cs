using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrainInterface
{
    public interface IBankAccount: IGrainWithGuidKey
    {
        Task AddCard(ICard card);
        Task<List<ICard>> GetCards();
    }
}
