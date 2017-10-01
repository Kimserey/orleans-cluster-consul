﻿using GrainInterfaces;
using Library;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("/self")]
        public async Task<IActionResult> Self()
        {
            var client = _grainFactoryResolver.Get(DeploymentConstants.ONE);
            var greeting = await client.GetGrain<IGrainOne>(Guid.Empty).SayHello();
            return Ok(greeting);
        }

        [HttpGet("/external")]
        public async Task<IActionResult> External()
        {
            var client = _grainFactoryResolver.Get(DeploymentConstants.TWO);
            var greeting = await client.GetGrain<IGrainTwo>(Guid.Empty).SayHello();
            return Ok(greeting);
        }
    }
}
