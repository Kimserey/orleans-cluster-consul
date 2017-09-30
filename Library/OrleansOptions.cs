using System.Collections.Generic;

namespace Library
{
    public class OrleansClusterOptions
    {
        public NodeConfigurationOptions Defaults { get; set; }
    }

    public class NodeConfigurationOptions
    {
        public IPEndPointOptions ProxyGatewayEndpoint { get; set; }
        public string HostNameOrIPAddress { get; set; }
        public int Port { get; set; }
    }

    public class IPEndPointOptions
    {
        public string Address { get; set; }
        public int Port { get; set; }
    }
}
