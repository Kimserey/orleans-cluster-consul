using GrainInterfaces;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using System;
using System.Threading.Tasks;

namespace OrleansHostApp1
{
    public class MyController : Controller
    {
        private readonly IGrainFactory _grainFactory;

        public MyController(IGrainFactory grainFactory)
        {
            _grainFactory = grainFactory;
        }

        [HttpGet("/hello")]
        public async Task<IActionResult> Self()
        {
            var grain = _grainFactory.GetGrain<IGrainOne>(Guid.Empty);
            var greeting = await grain.SayHello();
            return Ok(greeting);
        }
    }
}
