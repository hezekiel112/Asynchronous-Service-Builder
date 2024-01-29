using HezekDev.ServiceBuilder.Node;
using HezekDev.ServiceBuilder.Proxy;

namespace HezekDev.ServiceBuilder.Service {
    public class ServiceMain {
        public ServiceProxy[] Proxies => _proxies.ToArray();
        readonly List<ServiceProxy> _proxies = [];

        public async Task RunProxies() {
            List<Task> tasks = [];

            foreach (var node in Program.GetSvcNodeIntegrations()) {
                if (node.Node.GetNodePriorityExecution() == ENodePriorityExecution.INITIALIZE_ON_DEMAND) {

                    Program.SampleOutput($"system", $"The node '{node.Data.Name} is initialized on demand.\nMake sure to always include a node with an another priority than '{nameof(ENodePriorityExecution.INITIALIZE_ON_DEMAND)}' to ensure a working runloop", EMessageTypeColor.WARNING_IS_YELLOW);
                }
            }

            foreach (var proxy in _proxies) {
                for (byte i = 0; i < proxy.NodeRuntimeServices.Length; i++) {
                    ServiceNodeIntegration nodeIntegration = proxy.NodeRuntimeServices[i].ServiceNode;
                    Task task = nodeIntegration.ExecuteNodeAsync(false);

                    tasks.Add(Task.Run(() => task));
                }
            }

            await Task.WhenAll(tasks).ConfigureAwait(true);
        }

        public void AddProxies(ServiceProxy[] proxies) {
            foreach (var proxy in proxies) {
                _proxies.Add(proxy);
            }

            //ProgramOutput.Disp($"added {proxies.Length} proxy", EColor.GREEN);
        }
    }
}