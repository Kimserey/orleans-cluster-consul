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
            var grain = _factory.GetGrain<IBankAccountGrain>(Guid.Empty, "bankAccount");
            await grain.AddCard(cardNumber);
            return Ok();
        }
    }
}
