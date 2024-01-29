namespace HezekDev.ServiceBuilder.Node {
    using HezekDev.ServiceBuilder;

    public struct NodeData {
        public string Name {
            get;
        }

        public ENodePriorityExecution PriorityInProxy {
            get;
            set;
        } = ENodePriorityExecution.STANDARD;


        public NodeData(string name, ENodePriorityExecution priorityInProxy) {
            Name = name;
            PriorityInProxy = priorityInProxy;
        }
    }

}