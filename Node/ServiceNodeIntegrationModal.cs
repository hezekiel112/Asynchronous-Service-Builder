namespace HezekDev.ServiceBuilder.Node {
    using HezekDev.ServiceBuilder;
    using HezekDev.ServiceBuilder.Service;

    public abstract class ServiceNodeIntegrationModal : ServiceNode {
        public abstract ServiceHost[] Services {
            get;
        }

        public abstract ServiceNode Node {
            get;
        }

        readonly List<Task> tasks = [];

        public virtual IEnumerable<ServiceHost> GetServices() {
            foreach (var svc in Services) {
                yield return svc;
            }
        }

        public virtual async Task ExecuteServiceFromNodeAsync(string serviceName) {

            ServiceHost service = GetServices().FirstOrDefault(x => string.Equals(x.Data.Name, serviceName, StringComparison.Ordinal)) ?? null;

            if (service.Initialize() <= 0) {
                service.SetToken(ref service.Data, EServiceToken.SERVICE_IS_RUNNING);

                if (service.Data.Token == EServiceToken.SERVICE_IS_RUNNING) {
                    if (!service.Data.IsOneTimeExecution) {
                        Task task = service.ExecuteAsync();
                        tasks.Add(task);
                    }
                }
            }
            else {
                SystemOutput.Disp($"The service, '{service.Data.Name}' from node '{Node.Data.Name} 'has been skipped from node execution because it has not been correctly initialized", EMessageTypeColor.WARNING_IS_YELLOW);
                service.SetToken(ref service.Data, EServiceToken.SERVICE_IS_ABORTED);
            }

            await Task.WhenAll(tasks).ConfigureAwait(true);
        }

        public virtual async Task ExecuteServiceFromNodeAsync(ServiceHost service) {
            if (service.Initialize() == 0) {
                service.SetToken(ref service.Data, EServiceToken.SERVICE_IS_RUNNING);

                if (service.Data.Token == EServiceToken.SERVICE_IS_RUNNING) {
                    if (!service.Data.IsOneTimeExecution) {
                        Task task = service.ExecuteAsync();
                        tasks.Add(task);
                    }
                    else {
                        Task task = service.OneTimeExecute();
                        tasks.Add(task);
                    }
                }
            }
            else {
                SystemOutput.Disp($"The service, '{service.Data.Name}' from node '{Node.Data.Name} 'has been skipped from node execution because it has not been correctly initialized", EMessageTypeColor.WARNING_IS_YELLOW);
                service.SetToken(ref service.Data, EServiceToken.SERVICE_IS_ABORTED);
            }

            await Task.WhenAll(tasks).ConfigureAwait(true);
        }

        public virtual async Task ExecuteNodeAsync(bool forceExecution = false) {
            foreach (ServiceHost svc in GetServices()) {
                if (svc.Initialize() == 0) {
                    svc.SetToken(ref svc.Data, EServiceToken.SERVICE_IS_RUNNING);

                    if (svc.Data.Token == EServiceToken.SERVICE_IS_RUNNING && !svc.Data.IsOneTimeExecution) {
                        Task task = svc.ExecuteAsync();
                        tasks.Add(task);
                    }
                }
                else {
                    SystemOutput.Disp($"The service, '{svc.Data.Name}' from node '{Node.Data.Name} 'has been skipped from node execution because it has not been correctly initialized", EMessageTypeColor.WARNING_IS_YELLOW);
                    svc.Data.SetToken(EServiceToken.SERVICE_IS_ABORTED);
                }
            }

            await Task.WhenAll(tasks).ConfigureAwait(true);
        }
    }
}