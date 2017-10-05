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
    public class BankAccount : Grain<BankAccountState>, IBankAccountGrain
    {
        protected BankAccount() { }

        public Task AddCard(string cardNumber)
        {
            if (State.Cards == null)
            {
                State.Cards = new HashSet<string>();
            }

            State.Cards.Add(cardNumber);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<string>> GetCards(string cardNumber)
        {
            return Task.FromResult(State.Cards.AsEnumerable());
        }
    }

    public class BankAccountState
    {
        public HashSet<string> Cards { get; set; }
    }
}
