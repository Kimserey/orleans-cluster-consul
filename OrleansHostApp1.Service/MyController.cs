using GrainInterfaces;
using Library;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace OrleansHostApp1
{
    public class MyController : Controller
    {
        private readonly IOrleansClusterClientProvider _clientProvider;

        public MyController(IOrleansClusterClientProvider clientProvider)
        {
            _clientProvider = clientProvider;
        }

        [HttpGet("/self")]
        public async Task<IActionResult> Self()
        {
            var client = await _clientProvider.Get(DeploymentConstants.ONE);
            var greeting = await client.GetGrain<IGrainOne>(Guid.Empty).SayHello();
            return Ok(greeting);
        }

        [HttpGet("/external")]
        public async Task<IActionResult> External()
        {
            var client = await _clientProvider.Get(DeploymentConstants.TWO);
            var greeting = await client.GetGrain<IGrainTwo>(Guid.Empty).SayHello();
            return Ok(greeting);
        }
    }
}
