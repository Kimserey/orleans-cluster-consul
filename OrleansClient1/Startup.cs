using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Runtime.Configuration;

namespace OrleansClient1
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IGrainFactory>(sp => {
                GrainClient.Initialize(new ClientConfiguration
                {
                    DeploymentId = "cluster",
                    GatewayProvider = ClientConfiguration.GatewayProviderType.Custom,
                    DataConnectionString = "http://localhost:8500",
                    CustomGatewayProviderAssemblyName = "OrleansConsulUtils",
                    DefaultTraceLevel = Orleans.Runtime.Severity.Warning
                });

                return GrainClient.GrainFactory;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvcWithDefaultRoute();
        }
    }
}
