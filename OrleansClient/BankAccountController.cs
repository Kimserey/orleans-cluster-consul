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

        [HttpPost("Cards/{cardNumber}")]
        public async Task<IActionResult> AddCard(string bankKey, string cardNumber)
        {
            var grain = _factory.GetGrain<IBankAccount>(Guid.Empty);
            var card = _factory.GetGrain<ICard>(cardNumber);
            await card.Activate();
            await grain.AddCard(card);
            return Ok();
        }

        [HttpGet("Cards")]
        public async Task<IActionResult> Get()
        {
            var grain = _factory.GetGrain<IBankAccount>(Guid.Empty);
            var cards = await grain.GetCards();
            var numbers = await Task.WhenAll(cards.Select(c => c.GetCardNumber()));
            return Ok(numbers);
        }
    }
}
