using GrainInterface;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrleansClient
{
    [Route("BankAccount")]
    public class BankAccountController: Controller
    {
        private IGrainFactory _factory;

        public BankAccountController(IGrainFactory factory)
        {
            _factory = factory;
        }

        [HttpPost("{cardNumber}")]
        public async Task<IActionResult> AddCard(string cardNumber)
        {
            var grain = _factory.GetGrain<IBankAccount>(Guid.Empty);
            var card = _factory.GetGrain<ICard>(cardNumber);
            await card.Activate();
            await grain.AddCard(card);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var grain = _factory.GetGrain<IBankAccount>(Guid.Empty);
            var cards = await grain.GetCards();
            foreach (var card in cards)
            {
                _factory.BindGrainReference(card);
            }

            return Ok(cards.Select(c => c.IsActivated().Result));
        }
    }
}
