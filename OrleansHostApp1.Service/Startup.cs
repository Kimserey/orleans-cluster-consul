using Library;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Orleans.Runtime;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime;

namespace OrleansHostApp1
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<Serilog.ILogger>(serviceProvider =>
            {
                return new LoggerConfiguration()
                    .ReadFrom
                    .ConfigurationSection(Configuration.GetSection("Serilog"))
                    .CreateLogger();
            });

            services.Configure<OrleansClusterOptions>(Configuration.GetSection("Orleans:Cluster"));
            services.AddSingleton<IGrainFactoryResolver, GrainFactoryResolver>();
            services.AddSingleton(provider =>
            {
                var options = provider.GetService<IOptions<OrleansClusterOptions>>()?.Value;
                return SiloFactory.InitializeSilo(DeploymentConstants.ONE, options.Defaults.Port, options.Defaults.ProxyGatewayEndpoint.Port);
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory
                .AddConsole()
                .AddDebug();

            StartSilo(app);
            app.UseMvc();
        }

        public void StartSilo(IApplicationBuilder app)
        {
            var siloHost = app.ApplicationServices.GetRequiredService<SiloHost>();
            if (!siloHost.IsStarted)
            {
                throw new SystemException(String.Format("Failed to start Orleans silo '{0}' as a {1} node", siloHost.Name, siloHost.Type));
            }
        }
    }
}
