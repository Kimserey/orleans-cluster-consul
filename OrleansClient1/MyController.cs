using GrainInterfaces;
using Library;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using System;
using System.Threading.Tasks;

namespace OrleansHostApp1
{
    public class MyController : Controller
    {
        private readonly IGrainFactoryResolver _grainFactoryResolver;

        public MyController(IGrainFactoryResolver grainFactoryResolver)
        {
            _grainFactoryResolver = grainFactoryResolver;
        }

        [HttpGet("/clusterOne")]
        public async Task<IActionResult> CallOne()
        {
            var grain = _grainFactoryResolver.Get("clusterOne")
                .GetGrain<IGrainOne>(Guid.Empty);
            var greeting = await grain.SayHello();
            return Ok(greeting);
        }

        [HttpGet("/clusterTwo")]
        public async Task<IActionResult> CallTwo()
        {
            var grain = _grainFactoryResolver.Get("clusterTwo")
                .GetGrain<IGrainTwo>(Guid.Empty);
            var greeting = await grain.SayHello();
            return Ok(greeting);
        }
    }
}
