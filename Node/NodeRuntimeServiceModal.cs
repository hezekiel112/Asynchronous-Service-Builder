namespace HezekDev.ServiceBuilder.Node {

    using HezekDev.ServiceBuilder;
    using HezekDev.ServiceBuilder.Service;

    public abstract class NodeRuntimeServiceModal {
        public abstract ServiceHost[] Services {
            get;
        }

        public abstract ServiceNode ServiceNode {
            get;
        }

        public abstract NodeRuntimeServiceModal Instance {
            get;
        }

        readonly List<ServiceHost> _services = [];

        public virtual void CloseNode() {
            _services.Clear();
        }

        public virtual void AddIntoServices(ServiceHost host) {
            try {
                if (!_services.Contains(host)) {
                    _services.Add(host);
                }
            }
            catch {
                SystemOutput.Disp($"couldn't add {host.Data.Name} because it already exist in {ServiceNode.Data.Name}", EMessageTypeColor.WARNING_IS_YELLOW);
            }
        }

        public virtual void RemoveFromServices(ServiceHost host) {
            try {
                _services.Remove(host);
            }
            catch {
                SystemOutput.Disp($"couldn't remove '{host.Data.Name}' because it doesn't exist in '{ServiceNode.Data.Name}'", EMessageTypeColor.WARNING_IS_YELLOW);
            }
        }

        public virtual ServiceHost? GetFromServices(string name) {
            try {
                foreach (var svc in Services) {
                    if (string.Equals(svc.Data.Name, name, StringComparison.Ordinal)) {
                        return svc;
                    }
                }
            }
            catch {
                SystemOutput.Disp($"couldn't find '{name}' from '{this}'", EMessageTypeColor.ERROR_IS_RED);
            }

            return null;
        }
    }
}