# Orleans Cluster Consul

Orleans cluster communication with Consul membership.

- The project contains 2 services, 1 cluster with 1 silo per service.
- Consul as Membership provider

Cluster and client are contained within the same service.
For testing purposes a client is created and a connection is made on each hit of the controller endpoints.

# Issue with Consul

The following steps can be taken to reproduce the issue:

 1. Run consul
 2. Run OrleansHostApp1.Service
 3. Notice the silo being registered properly as alive in consul
 ![1](https://github.com/Kimserey/orleans-cluster-consul/blob/master/OrleansHostApp1.Service/img/1_run_project.PNG?raw=true)
 4. Invoke http://localhost:20007/self
 5. Ctrl+c to close the project
 6. Notice a key being registered in consul using the proxy gateway address with it being marked as dead (status 6), the silo address is still alive (status 3)
 ![2](https://raw.githubusercontent.com/Kimserey/orleans-cluster-consul/master/OrleansHostApp1.Service/img/2_ctrl_c.PNG)
