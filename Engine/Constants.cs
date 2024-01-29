using System.Reflection;

namespace HezekDev.ServiceBuilder {
    public sealed record Constants {
        public const string ERROR_MISSING_SVC_NODE_INTEGRATION = "ServiceNodeIntegration is not initialized. It is required";
        public const string ERROR_MISSING_NODE_RUNTIME_SVC = "NodeRuntimeService is not initialized. It is required";
        public const string ERROR_MISSING_SVC_PROXY_MGR = "ServiceProxyManager is not initialized. It is required";
        public const string ERROR_MISSING_SVC_MAIN = "ServiceMain is not initialized. It is required";
        public const string ERROR_WATCH_DOG_NOT_INCLUDED = "WatchDog will be supported in the next update.\nIf you want to test this functionnality, visit the ServiceBuilderBeta repositery.";

        public static readonly string ABSOLUTE_FILE_SAVE_PATH = Assembly.GetExecutingAssembly().Location;
    }
}