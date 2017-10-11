using GrainInterface;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrleansClient
{
    [Route("Bank")]
    public class BankController : Controller
    {
        private IGrainFactory _factory;

        public BankController(IGrainFactory factory)
        {
            _factory = factory;
        }

        [HttpPost("{bankKey}/Name/{name}")]
        public async Task<IActionResult> CreateBank(string bankKey, string name)
        {
            var grain = _factory.GetGrain<IBank>(bankKey);
            await grain.SetName(name);
            return Ok();
        }

        [HttpPost("{bankKey}/Customers/{customerKey}")]
        public async Task<IActionResult> AddCustomer(string bankKey, string customerKey)
        {
            var bank = _factory.GetGrain<IBank>(bankKey);
            var customer = _factory.GetGrain<ICustomer>(customerKey);
            await customer.SetName(customerKey);
            await bank.AddCustomer(customer);
            return Ok();
        }

        [HttpGet("{bankKey}/Customers")]
        public async Task<IActionResult> Get(string bankKey)
        {
            var bank = _factory.GetGrain<IBank>(bankKey);
            var customers = await bank.GetCustomers();
            var names = await Task.WhenAll(customers.Select(c => c.GetName()));
            return Ok(names);
        }
    }
}
