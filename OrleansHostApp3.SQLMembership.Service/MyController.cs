using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library;
using GrainInterfaces;

namespace OrleansHostApp2
{
    public class MyController: Controller
    {
        private readonly IGrainFactoryResolver _grainFactoryResolver;

        public MyController(IGrainFactoryResolver grainFactoryResolver)
        {
            _grainFactoryResolver = grainFactoryResolver;
        }

        [HttpGet("/self")]
        public async Task<IActionResult> Self()
        {
            var client = _grainFactoryResolver.Get(DeploymentConstants.ONE);
            var greeting = await client.GetGrain<IGrainOne>(Guid.Empty).SayHello();
            return Ok(greeting);
        }
    }
}
