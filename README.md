# Orleans Cluster Consul

Orleans cluster communication with Consul membership.

- The project contains 2 services, 1 cluster with 1 silo per service.
- Consul as Membership provider

Cluster and client are contained within the same service.
For testing purposes a client is created and a connection is made on each hit of the controller endpoints.