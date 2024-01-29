namespace HezekDev.ServiceBuilder.Proxy {
    public class ServiceProxyManager {
        public ServiceProxy[] ServiceProxies => _serviceProxies.ToArray();

        readonly List<ServiceProxy> _serviceProxies = [];

        public void AddProxy(ServiceProxy proxy) {
            try {
                if (!_serviceProxies.Contains(proxy)) {
                    _serviceProxies.Add(proxy);
                }
            }
            catch {
                SystemOutput.Disp($"couldn't add '{proxy}' because it already exist in {_serviceProxies}", EMessageTypeColor.ERROR_IS_RED);
            }
        }

        public void RemoveProxy(ServiceProxy proxy) {
            try {
                _serviceProxies.Remove(proxy);
            }
            catch {
                SystemOutput.Disp($"couldn't remove '{proxy}' because it doesn't exist in '{_serviceProxies}'", EMessageTypeColor.ERROR_IS_RED);
            }
        }
    }

}