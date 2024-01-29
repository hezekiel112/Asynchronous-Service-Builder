using HezekDev.ServiceBuilder.Service;
using HezekDev.ServiceBuilder.Node;

namespace HezekDev.ServiceBuilder {
    [Serializable]
    public struct ServiceExecutionTrace {
        public byte? ResultKey {
            get; set;
        }

        public ServiceData? Host {
            get;set;
        }

        public ServiceNode? Node {
            get; set;
        }

        public string? Message {
            get;set;
        }
    }
}