namespace HezekDev.ServiceBuilder.Node {
    using HezekDev.ServiceBuilder;

    public class ServiceNode : ServiceNodeModal {
        public required NodeData Data {
            get;
            set;
        }

        public override ENodePriorityExecution GetNodePriorityExecution() {
            return Data.PriorityInProxy;
        }
    }
}