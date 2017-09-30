using System.Collections.Generic;

namespace Library
{
    public class OrleansClusterOptions
    {
        public GlobalConfigurationOptions Globals { get; set; }
        public NodeConfigurationOptions Defaults { get; set; }
    }

    public class GlobalConfigurationOptions
    {
        public string DeploymentId { get; set; }
        public string LivenessType { get; set; }
        public string DataConnectionString { get; set; }
        public string ReminderServiceType { get; set; }
        public string DataConnectionStringForReminders { get; set; }
        public string AdoInvariantForReminders { get; set; }
        public OrleansProvider[] StorageProviders { get; set; }
        public OrleansProvider[] BootstrapProviders { get; set; }
        public string MembershipTableAssembly { get; set; }
    }

    public class OrleansProvider
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string DataConnectionString { get; set; }
        public Dictionary<string, string> Properties { get; set; }
    }

    public class NodeConfigurationOptions
    {
        public IPEndPointOptions ProxyGatewayEndpoint { get; set; }
        public bool TraceToConsole { get; set; }
        public string HostNameOrIPAddress { get; set; }
        public int Port { get; set; }
        public string TraceFileName { get; set; }
        public string TraceFilePattern { get; set; }
        public string DefaultTraceLevel { get; set; }
    }

    public class IPEndPointOptions
    {
        public string Address { get; set; }
        public int Port { get; set; }
    }
}
