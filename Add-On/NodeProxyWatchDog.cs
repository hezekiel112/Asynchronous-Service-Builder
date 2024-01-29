#if WATCH_DOG_START
using HezekDev.ServiceBuilder.Node;
using HezekDev.ServiceBuilder.Service;

namespace HezekDev.ServiceBuilder.AddOn {
    /// <summary>
    /// The watchdog is a plugin made for ServiceBuilder, allowing to debug user's custom services and built-in one
    /// </summary>
    public class NodeProxyWatchDog {
        public static NodeProxyWatchDogSocket[] Sockets => _sockets.ToArray();
        static List<NodeProxyWatchDogSocket> _sockets = new();

        public static NodeProxyWatchDogSocket MakeOneTime(ServiceHost[] hosts, ServiceNode local) {
            SystemOutput.Disp(Constants.ERROR_WATCH_DOG_NOT_INCLUDED);
            throw new NotSupportedException();
        }

        public static async Task<NodeProxyWatchDogSocket> LinkRuntimeHook(ServiceHost[] hosts, ServiceNode local) {
            SystemOutput.Disp(Constants.ERROR_WATCH_DOG_NOT_INCLUDED);
            await Task.CompletedTask.ConfigureAwait(true);
            throw new NotSupportedException();
        }
    }
}
#endif