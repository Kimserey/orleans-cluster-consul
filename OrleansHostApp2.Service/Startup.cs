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

namespace OrleansHostApp2
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

            ConfigureOrleans(services);
            services.AddMvc();
        }

        public void ConfigureOrleans(IServiceCollection services)
        {
            services.Configure<OrleansClusterOptions>(Configuration.GetSection("Orleans:Cluster"));

            services.AddSingleton<IOrleansClusterClientProvider, OrleansClusterClientProvider>();

            services.AddSingleton(provider =>
            {
                var options = provider.GetService<IOptions<OrleansClusterOptions>>()?.Value;
                var config = new ClusterConfiguration();
                config.Globals.SetGlobalsForConsul(DeploymentConstants.TWO);
                config.Defaults.SetDefaults(options.Defaults);
                LogManager.LogConsumers.Add(new OrleansLoggerFactoryAdapter(provider.GetService<ILoggerFactory>()));

                var siloHost = new SiloHost(Dns.GetHostName(), config);
                return siloHost;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            var logger = app.ApplicationServices.GetRequiredService<Serilog.ILogger>();
            loggerFactory.AddSerilog(logger, true);
            //loggerFactory.AddDebug();

            StartSilo(app);
            app.UseMvc();
        }

        public void StartSilo(IApplicationBuilder app)
        {
            if (!GCSettings.IsServerGC) throw new InvalidProgramException("Server GC should be enabled for orleans");
            var siloHost = app.ApplicationServices.GetRequiredService<SiloHost>();
            siloHost.InitializeOrleansSilo();
            var startedOk = siloHost.StartOrleansSilo();
            if (!startedOk)
            {
                throw new SystemException(String.Format("Failed to start Orleans silo '{0}' as a {1} node", siloHost.Name, siloHost.Type));
            }
        }
    }
}