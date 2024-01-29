#if WATCH_DOG_START
using HezekDev.ServiceBuilder.Node;
using HezekDev.ServiceBuilder.Service;

namespace HezekDev.ServiceBuilder.AddOn {

    public struct NodeProxyWatchDogSocket {
        public int SocketID {
            get;
        }

        public string SocketInformation {
            get;
        }

        public ServiceHost[] FromHostInformation {
            get;
        }

        public ServiceNode FromNode {
            get;
        }

        public NodeProxyWatchDogSocket(int id, string information, ServiceHost[] hostInformations, ServiceNode local) {
            SocketID = id;
            SocketInformation = information;
            FromHostInformation = hostInformations;
            FromNode = local;
        }
    }
}
#endif