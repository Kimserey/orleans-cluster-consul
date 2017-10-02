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
using System;
using System.Net;
using System.Runtime;

namespace OrleansHostApp3.SQLMembership.Service
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
            IConfigurationRoot Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureOrleans(services);
            services.AddMvc();
        }

        public void ConfigureOrleans(IServiceCollection services)
        {
            services.Configure<OrleansClusterOptions>(Configuration.GetSection("Orleans:Cluster"));

            services.AddSingleton<IGrainFactoryResolver, GrainFactoryResolver>();

            services.AddSingleton(provider =>
            {
                var options = provider.GetService<IOptions<OrleansClusterOptions>>()?.Value;
                var config = new ClusterConfiguration();
                config.Globals.SetGlobalsForConsul(options.Globals.DeploymentId);
                config.Defaults.SetDefaults(options.Defaults);

                var siloHost = new SiloHost(Dns.GetHostName(), config);
                return siloHost;
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            StartSilo(app);
            app.UseMvcWithDefaultRoute();
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
