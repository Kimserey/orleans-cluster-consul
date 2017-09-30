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
        public static void SetGlobalsForConsul(this GlobalConfiguration globals, string deploymentId)
        {
            globals.DeploymentId = deploymentId;
            globals.LivenessType = GlobalConfiguration.LivenessProviderType.Custom;
            globals.DataConnectionString = "http://localhost:8500";
            globals.MembershipTableAssembly = "OrleansConsulUtils";
            globals.ReminderServiceType = GlobalConfiguration.ReminderServiceProviderType.Disabled;
        }

        public static void SetDefaults(this NodeConfiguration config, NodeConfigurationOptions options)
        {
            IPAddress ResolveIPAddress(string hostNameOrIPAddress)
            {
                return Dns.GetHostAddresses(hostNameOrIPAddress).First(x => x.AddressFamily == AddressFamily.InterNetwork);
            }

            var proxyGatewayEndpoint = new IPEndPoint(ResolveIPAddress(options.ProxyGatewayEndpoint.Address), options.ProxyGatewayEndpoint.Port);
            config.ProxyGatewayEndpoint = proxyGatewayEndpoint;
            config.TraceToConsole = true;
            config.DefaultTraceLevel = Severity.Info;

            config.HostNameOrIPAddress = options.HostNameOrIPAddress;
            config.Port = options.Port;
        }
    }
}
