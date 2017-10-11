using GrainInterface;
using Orleans;
using Orleans.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace GrainWithState
{
    [StorageProvider(ProviderName = "store")]
    public class BankAccount : Grain<BankAccountState>, IBankAccount
    {
        public BankAccount() { }

        public Task Test(Test x)
        {
            return Task.CompletedTask;
        }

        public async Task AddCard(ICard card)
        {
            if (!await card.IsActivated())
            {
                throw new Exception("Card must be activated first.");
            }

            if (State.Cards == null)
            {
                State.Cards = new List<ICard>();
            }
            else if (State.Cards.Contains(card))
            {
                return;
            }

            State.Cards.Add(card);
            await WriteStateAsync();
        }

        public Task<List<ICard>> GetCards()
        {
            return Task.FromResult(State.Cards);
        }
    }

    public class BankAccountState
    {
        public List<ICard> Cards { get; set; }
    }
}
