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

        public static void SetDefaults(this NodeConfiguration config, NodeConfigurationOptions options)
        {
            IPAddress ResolveIPAddress(string hostNameOrIPAddress)
            {
                return Dns.GetHostAddresses(hostNameOrIPAddress).First(x => x.AddressFamily == AddressFamily.InterNetwork);
            }

            var address = ResolveIPAddress(options.ProxyGatewayEndpoint.Address);
            var proxyGatewayEndpoint = new IPEndPoint(address, options.ProxyGatewayEndpoint.Port);
            config.ProxyGatewayEndpoint = proxyGatewayEndpoint;
            config.DefaultTraceLevel = Severity.Warning;
            config.HostNameOrIPAddress = options.HostNameOrIPAddress;
            config.Port = options.Port;
            config.TraceToConsole = true;
        }
    }
}
