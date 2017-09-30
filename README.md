# Orleans Cluster Consul

Orleans cluster communication with Consul membership.

- The project contains 2 services, 1 cluster with 1 silo per service.
- Consul as Membership provider

Cluster and client are contained within the same service.
For testing purposes a client is created and a connection is made on each hit of the controller endpoints.

Current error:

1. run App1 only
2. invoke /self
3. invoke /external
4. every minutes error appears
5. in Consul, under orleans/app1/+, IAmAlive is stored under Proxy address

```
[16:26:50 ERR] Connection id "0HL87RIH6FVF5": An unhandled exception was thrown by the application.
Orleans.Runtime.OrleansException: Could not find any gateway in Orleans.Runtime.Host.ConsulBasedMembershipTable. Orleans client cannot initialize.
   at Orleans.Messaging.GatewayManager..ctor(ClientConfiguration cfg, IGatewayListProvider gatewayListProvider)
   at Orleans.Messaging.ProxiedMessageCenter..ctor(ClientConfiguration config, IPAddress localAddress, Int32 gen, GrainId clientId, IGatewayListProvider gatewayListProvider, SerializationManager serializationManager, IRuntimeClient runtimeClient, MessageFactory messageFactory, IClusterConnectionStatusListener connectionStatusListener)
--- End of stack trace from previous location where exception was thrown ---
   at Microsoft.Extensions.Internal.ActivatorUtilities.ConstructorMatcher.CreateInstance(IServiceProvider provider)
   at Microsoft.Extensions.Internal.ActivatorUtilities.CreateInstance(IServiceProvider provider, Type instanceType, Object[] parameters)
   at Microsoft.Extensions.Internal.ActivatorUtilities.CreateInstance[T](IServiceProvider provider, Object[] parameters)
   at Orleans.OutsideRuntimeClient.<StartInternal>d__65.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Orleans.OutsideRuntimeClient.<Start>d__64.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Orleans.ClusterClient.<Connect>d__24.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.GetResult()
   at Library.OrleansClusterClientProvider.<Get>d__0.MoveNext() in C:\Projects\OrleansClusterConsul\Library\OrleansClusterClientProvider.cs:line 15
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter`1.GetResult()
   at OrleansHostApp1.MyController.<External>d__3.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeActionMethodAsync>d__27.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeNextActionFilterAsync>d__25.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ActionExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeNextResourceFilter>d__22.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Rethrow(ResourceExecutedContext context)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker.<InvokeAsync>d__20.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Builder.RouterMiddleware.<Invoke>d__4.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at Microsoft.AspNetCore.Hosting.Internal.RequestServicesContainerMiddleware.<Invoke>d__3.MoveNext()
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at System.Runtime.CompilerServices.TaskAwaiter.ValidateEnd(Task task)
   at Microsoft.AspNetCore.Server.Kestrel.Internal.Http.Frame`1.<RequestProcessingAsync>d__2.MoveNext()
```