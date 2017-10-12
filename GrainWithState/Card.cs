using System;
using System.Threading.Tasks;
using GrainInterface;
using Orleans;
using Orleans.Providers;

namespace GrainWithState
{
    [StorageProvider(ProviderName = "store")]
    public class Card : Grain<CardState>, ICard
    {
        public Task<bool> IsActivated()
        {
            return Task.FromResult(State.Activation < DateTime.Now);
        }

        public Task Activate()
        {
            State.Activation = DateTime.Now;
            return WriteStateAsync();
        }

        public Task SetExpiry(DateTime date)
        {
            State.Expiry = date;
            return WriteStateAsync();
        }

        public Task SetName(string name)
        {
            State.Name = name;
            return WriteStateAsync();
        }

        public Task<string> GetCardNumber()
        {
            return Task.FromResult(this.GetPrimaryKeyString());
        }
    }

    public class CardState
    {
        public DateTime Activation { get; set; }
        public string Name { get; set; }
        public DateTime Expiry { get; set; }
    }
}
