# Asynchronous-Service-Builder
C# library used for making asynchronous background service

# Features
  - Fully asynchronous toolchain
  - Made for large scale project (Working with a Proxy network along with linked Node service cluster)
  - Simple API (A single Proxy, Node and Service will kickstart the system)
  - Token system for service (Each service dispose a Token representing to their state)
  - Runtime Processed (There's no limitation to what you can do at runtime, the system is dynamic)
  - Exception Handler (Each services is handled for any exception that can occure and they can be restarted)
  - Production Ready (The system is battle tested for production release) *Make sure to check the latest build issues reports
  - Exit Key (Each service's RunLoop has an exit key when their function ended) *0 = PASS, 1 = ERROR (need manual restart), 2 = ERROR (automatic restart)
  - Run Priority (Proxies and Nodes has their own priority on boot, which can be manual and need a manual start) *HIGH = 0x3, MEDIUM = 0x2, STANDARD = 0x1, MANUAL)
  - Engine's Defaults (The project contains samples services which can help in kickstart your project) *Simple REPL and MANUAL priority node contains full code documentation

# Instruction
  - In order to try out ASB, you will need to swich into the <b>alpha</b> branch.
