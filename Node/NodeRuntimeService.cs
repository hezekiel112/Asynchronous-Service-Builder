namespace HezekDev.ServiceBuilder.Node {
    using HezekDev.ServiceBuilder.Service;

    public class NodeRuntimeService : NodeRuntimeServiceModal {
        public override ServiceHost[] Services {
            get;
        }

        public override ServiceNodeIntegration ServiceNode {
            get;
        }

        public override NodeRuntimeService Instance {
            get;
        }

        public override void CloseNode() {
            base.CloseNode();
        }

        public override void AddIntoServices(ServiceHost host) {
#if WATCH_DOG_START
            SystemOutput.Disp($"{host.Data.Name} was added to {this.ServiceNode.Data.Name}");
#endif
            base.AddIntoServices(host);
        }

        public override ServiceHost? GetFromServices(string name) {
            return base.GetFromServices(name);
        }

        public override void RemoveFromServices(ServiceHost host) {
#if WATCH_DOG_START
            SystemOutput.Disp($"{host.Data.Name} was removed from {this.ServiceNode.Data.Name}");
#endif
            base.RemoveFromServices(host);
        }

        public NodeRuntimeService(ServiceNodeIntegration node) {
            ServiceNode = node;
            Services = node.Services;
            Instance = this;
        }
    }
}