using System.Text;
using HezekDev.ServiceBuilder.Node;
using HezekDev.ServiceBuilder.Proxy;
using HezekDev.ServiceBuilder.Service;

namespace HezekDev.ServiceBuilder {
    sealed internal class Program {
        static ServiceNodeIntegration[]? Nodes;
        static NodeRuntimeService[]? RuntimeNodes;
        static ServiceProxyManager? ProxyManager;
        static ServiceMain? ServiceMain;

        static async Task Main(string[] args) { 
            var vincent = new NodeData("vincent_node", ENodePriorityExecution.HIGH);
            var caca = new NodeData("caca_node", ENodePriorityExecution.INITIALIZE_ON_DEMAND);

            Nodes = [
#if !RELEASE
                new ServiceNodeIntegration(DefaultEngine_Svc.DebugServices) {
                    Data = svcNodeDebug,
                },
#endif
#if RELEASE
                new ServiceNodeIntegration(DefaultEngine_Svc.ReleaseServices) {
                    Data = vincent,
                },
                new ServiceNodeIntegration(DefaultEngine_Svc.Services) {
                    Data = caca,
                }
#endif
            ];

            RuntimeNodes = [
#if !RELEASE
                new NodeRuntimeService(Nodes[0]),
#endif
#if RELEASE
                new NodeRuntimeService(Nodes[0]),
                new NodeRuntimeService(Nodes[1]),
#endif
            ];

            ProxyManager = new ServiceProxyManager();
            ProxyManager.AddProxy(new ServiceProxy(RuntimeNodes));

            ServiceMain = new ServiceMain();
            ServiceMain.AddProxies(ProxyManager.ServiceProxies);

            List<Task> tasks = [
                ServiceMain.RunProxies(),
            ];

#if RELEASE
            SystemOutput.Disp("This build is a release build. \nIn order to debug your nodes or services, please start the console using the -debug startup parameter.\n");
#endif
#if !RELEASE
            SystemOutput.Disp("This build is a debug build. this mod enable watchdog and allows the console to receives runtime output sorted by your services.\n");
#endif

            try {
                await Task.WhenAll(tasks).ConfigureAwait(true);
            }
            catch (Exception ex){
                SystemOutput.Disp($"fatal error occured while executing core function.\n please restart application ...{ex.Message}", EMessageTypeColor.ERROR_IS_RED);
            }
        }

        static ServiceHost ExecutionMaker(ServiceData data, Func<byte> execution) {
            return new(data, execution);
        }

        public static ServiceNodeIntegration[] GetSvcNodeIntegrations() {
            return Nodes ?? throw new Exception(Constants.ERROR_MISSING_SVC_NODE_INTEGRATION);
        }

        public static NodeRuntimeService[] GetNodeRuntimeServices() {
            return RuntimeNodes ?? throw new Exception(Constants.ERROR_MISSING_NODE_RUNTIME_SVC);
        }

        public static ServiceProxyManager GetSvcProxyManager() {
            return ProxyManager ?? throw new Exception(Constants.ERROR_MISSING_SVC_PROXY_MGR);
        }

        public static ServiceMain GetSvcMain() {
            return ServiceMain ?? throw new Exception(Constants.ERROR_MISSING_SVC_MAIN);
        }

        public static ServiceNode? GetNodeFromService(ServiceHost service) {
            try {
                foreach (var node in GetNodeRuntimeServices()) {
                    if (node.Services.Contains(service))
                        return node.Instance.ServiceNode;
                }
            }
            catch {
                SystemOutput.Disp($"could not refer node of service, '{service.Data.Name}' ... is your node properly initialized ?");
            }

            return null;
        }

        public async static Task LoggerAsync(ServiceExecutionTrace trace) {

            List<ServiceExecutionTrace> traces = new() {
                trace,
            };

            await Task.Run(() => {
                DefaultEngine_ProgramUser.Save([.. traces]);
            }).ConfigureAwait(true);
        }

        public static void Logger(ServiceExecutionTrace trace) {

            List<ServiceExecutionTrace> traces = new() {
                trace,
            };

            DefaultEngine_ProgramUser.Save([.. traces]);
        }

        public static void SampleOutput(ServiceHost from, string message, EMessageTypeColor color = EMessageTypeColor.INFO_IS_WHITE) {
            SystemOutput.Disp($"<{from.Data.Name}> : {message.ToLowerInvariant()}", color);
        }

        public static void SampleOutput(string from, string message, EMessageTypeColor color = EMessageTypeColor.INFO_IS_WHITE) {
            SystemOutput.Disp($"<{from.ToLowerInvariant()}> : {message.ToLowerInvariant()}", color);
        }
    }
}