using HezekDev.ServiceBuilder.Node;
using HezekDev.ServiceBuilder.Service;

namespace HezekDev.ServiceBuilder.Proxy {
    public class ServiceProxy : ServiceProxyModal {
        public override NodeRuntimeService[] NodeRuntimeServices {
            get;
        }

        public override void AddIntoNode(NodeRuntimeService node, ServiceHost host) {
            base.AddIntoNode(node, host);
        }

        public override void RemoveFromNode(NodeRuntimeService node, ServiceHost host) {
            base.RemoveFromNode(node, host);
        }

        public ServiceProxy(NodeRuntimeService[] nodeRuntimeService) {
            NodeRuntimeServices = nodeRuntimeService;
        }
    }
}
