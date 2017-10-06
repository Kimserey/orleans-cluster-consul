using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime.Configuration;
using System.Net;

namespace OrleansClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IGrainFactory>(sp =>
            {
                var client =
                    new ClientBuilder()
                        .UseConfiguration(new ClientConfiguration
                        {
                            DeploymentId = "main",
                            Gateways = new List<IPEndPoint>{
                                new IPEndPoint(IPAddress.Loopback, 40023)
                            }
                        })
                        .Build();

                client.Connect().Wait();

                return client;
            });
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole().AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ApplicationServices.GetRequiredService<IGrainFactory>();
            app.UseMvcWithDefaultRoute();
        }
    }
}
