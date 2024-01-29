using HezekDev.ServiceBuilder.Node;
using HezekDev.ServiceBuilder.Service;

namespace HezekDev.ServiceBuilder.Proxy {
    public abstract class ServiceProxyModal {
        public abstract NodeRuntimeService[] NodeRuntimeServices {
            get;
        }

        public virtual void AddIntoNode(NodeRuntimeService node, ServiceHost host) {
            node.AddIntoServices(host);
        }

        public virtual void RemoveFromNode(NodeRuntimeService node, ServiceHost host) {
            node.RemoveFromServices(host);
        }

        public virtual IEnumerable<ServiceHost> GetServicesFromNode(NodeRuntimeService node) {
            foreach (var svc in node.Services)
                yield return svc;
        }
    }

}