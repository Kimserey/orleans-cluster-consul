using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library;
using GrainInterfaces;
using Orleans.Runtime.Configuration;

namespace OrleansHostApp4.ZKMembership.Service
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
            var client = _grainFactoryResolver.Get("AppZkMembership",
                    ClientConfiguration.GatewayProviderType.ZooKeeper,
                    "http://localhost:2181",
                    "OrleansZooKeeperUtils");
            var greeting = await client.GetGrain<IGrainOne>(Guid.Empty).SayHello();
            return Ok(greeting);
        }
    }
}
