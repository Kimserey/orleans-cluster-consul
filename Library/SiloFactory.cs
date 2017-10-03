using Orleans.Runtime;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using System;
using System.Net;

namespace Library
{
    public static class SiloFactory
    {
        public static SiloHost InitializeSilo(string deploymentId, int port, int proxy, Action<GlobalConfiguration> configureMembershipProvider = null)
        {
            var config = new ClusterConfiguration();
            config.Globals.DeploymentId = deploymentId;
            config.Globals.ReminderServiceType = GlobalConfiguration.ReminderServiceProviderType.Disabled;

            if (configureMembershipProvider == null)
            {
                config.Globals.SetGlobals();
            }
            else
            {
                configureMembershipProvider(config.Globals);
            }

            config.Defaults.SetDefaults("localhost", port, "localhost", proxy, true, Severity.Info);

            var siloHost = new SiloHost($"{Dns.GetHostName()}-{port}", config);
            siloHost.InitializeOrleansSilo();

            var startedOk = siloHost.StartOrleansSilo();
            if (!startedOk)
            {
                throw new SystemException(String.Format("Failed to start Orleans silo '{0}' as a {1} node", siloHost.Name, siloHost.Type));
            }
            return siloHost;
        }
    }
}
