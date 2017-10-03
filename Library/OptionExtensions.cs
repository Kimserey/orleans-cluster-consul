using Orleans.Runtime;
using Orleans.Runtime.Configuration;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace Library
{
    public static class OptionExtensions
    {
        public static void SetGlobalsForConsul(this GlobalConfiguration globals, 
            string deploymentId, 
            GlobalConfiguration.LivenessProviderType liveness = GlobalConfiguration.LivenessProviderType.Custom,
            string dataConnectionString = "http://localhost:8500",
            string membershipTableAssembly = "OrleansConsulUtils")
        {
            globals.DeploymentId = deploymentId;
            globals.LivenessType = liveness;
            globals.DataConnectionString = dataConnectionString;
            globals.MembershipTableAssembly = membershipTableAssembly;
            globals.ReminderServiceType = GlobalConfiguration.ReminderServiceProviderType.Disabled;
        }

        public static void SetDefaults(this NodeConfiguration config, 
            string siloAddress = "localhost", 
            int siloPort = 0, 
            string proxyGatewayAddress = "localhost",
            int proxyGatewayPort= 0,
            bool traceToConsole = true,
            Severity defaultTraceLevel = Severity.Warning)
        {
            config.HostNameOrIPAddress = siloAddress;
            config.Port = siloPort;
            config.ProxyGatewayEndpoint = new IPEndPoint(IPAddress.Parse(proxyGatewayAddress), proxyGatewayPort);
            config.DefaultTraceLevel = defaultTraceLevel;
            config.TraceToConsole = traceToConsole;
        }
    }
}
