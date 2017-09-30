# Orleans Cluster Consul

Orleans cluster communication with Consul membership.

- The project contains 2 services, 1 cluster with 1 silo per service.
- Consul as Membership provider

Cluster and client are contained within the same service.
For testing purposes a client is created and a connection is made on each hit of the controller endpoints.

## Current errors:

### 1. An item with the same key has already been added

1. run App1 only
2. invoke /self
3. invoke /external
4. every minutes error appears
5. in Consul, under orleans/app1/+, IAmAlive is stored under Proxy address

```
Exc level 0: System.ArgumentException: An item with the same key has already been added.
   at System.ThrowHelper.ThrowArgumentException(ExceptionResource resource)
   at System.Collections.Generic.Dictionary`2.Insert(TKey key, TValue value, Boolean add)
   at Orleans.Runtime.MembershipService.MembershipOracleData.GetSiloStatuses(Func`2 filter, Boolean includeMyself)
   at Orleans.Runtime.MembershipService.MembershipOracleData.TryUpdateStatusAndNotify(MembershipEntry entry)
   at Orleans.Runtime.MembershipService.MembershipOracle.<ProcessTableUpdate>d__53.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Orleans.Runtime.MembershipService.MembershipOracle.<<OnGetTableUpdateTimer>b__62_0>d.MoveNext()
```


1. 
