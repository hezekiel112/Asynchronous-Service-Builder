namespace HezekDev.ServiceBuilder.Node {
    using HezekDev.ServiceBuilder;
    using HezekDev.ServiceBuilder.Service;

    public class ServiceNodeIntegration : ServiceNodeIntegrationModal {
        public override ServiceHost[] Services {
            get;
        }

        public override ServiceNode Node => this;

        public override ENodePriorityExecution GetNodePriorityExecution() {
            return Data.PriorityInProxy;
        }

        public override Task ExecuteServiceFromNodeAsync(string serviceName) {
            return base.ExecuteServiceFromNodeAsync(serviceName);
        }

        public override Task ExecuteServiceFromNodeAsync(ServiceHost service) {
            return base.ExecuteServiceFromNodeAsync(service);
        }

        public override Task ExecuteNodeAsync(bool forceExecution = false) {
            if (!forceExecution && GetNodePriorityExecution() == ENodePriorityExecution.INITIALIZE_ON_DEMAND)
                return Task.CompletedTask;

            for (byte i = 3; i >= 0; i--) {
                if (i == (byte) GetNodePriorityExecution()) {
                    SystemOutput.Disp($"started node '{Data.Name}' with priority {GetNodePriorityExecution()}", EMessageTypeColor.SUCCESS_IS_GREEN);

                    foreach (var svc in Services)
                    {
                        svc.SetToken(ref svc.Data, EServiceToken.SERVICE_IS_IDLE);
                    }

                    return base.ExecuteNodeAsync();
                }
            }

            return Task.CompletedTask;
        }

        public ServiceNodeIntegration(ServiceHost[] services) {
            Services = services;
        }
    }
}