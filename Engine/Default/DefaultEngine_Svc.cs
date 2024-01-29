namespace HezekDev.ServiceBuilder {
    using HezekDev.ServiceBuilder.Node;
    using HezekDev.ServiceBuilder.Service;

    /// <summary>
    /// default built-in services
    /// </summary>
    public record DefaultEngine_Svc {
       static Action<ServiceHost, string, EMessageTypeColor> SampleOutput => Program.SampleOutput;

#if !RELEASE
        public static readonly ServiceHost[] DebugServices = [
             new(new ServiceData("app_rl", "send an output every three seconds to check if the runloop is still active", 3), () => {
                byte exitCode = 0;

                try {
#if ALLOW_LOGGING
                    SampleOutput(DebugServices[0], "service runloop is working...", EMessageTypeColor.SUCCESS_IS_GREEN);
#endif
                    return exitCode;
                }
                catch {
                    return exitCode++;
                }
            }),
        ];
#endif

#if RELEASE
        public static readonly ServiceHost[] ReleaseServices = [
             new(new ServiceData("input", "service for the input runtime", isOneTimeExecution: false), () => {
                 byte exitCode = 0;

                 
                 string? input = Console.ReadLine();
                 string inputPrimary = input.Split(" ")[0];

                 string nodeName = string.Empty,
                 serviceName = string.Empty;

                 try {
                     switch (inputPrimary) {
                         case "help":

                             SystemOutput.Disp($"restart_svc: redemarre le service d'entree/sortie utilisateur\n" +
                                 $"get_info: renvois le ticket du service\n" +
                                 $"close_svc: ferme un service [close_svc: (nom_node) (nom_service)]\n" +
                                 $"run_svc: ouvre un service [run_svc: (nom_node) (nom_service]\n" +
                                 $"run_node: ouvre un node [run_node: (nom_node)]\n" +
                                 $"close_node: ferme un node [close_node: (nom_node)]\n" +
                                 $"Attention! malgrés les précautions, certaine commande influe sur le service actuel et peu le rendre inutilisable.\n");
                             break;

                         case "restart_svc":
                             ReleaseServices[0].Restart();
                             break;

                         case "get_info":
                             SystemOutput.Disp(ReleaseServices[0].Data.Token.ToString());
                             break;

                         case "close_svc":
                             var closeSvcNode = Program.GetNodeRuntimeServices().FirstOrDefault(x => string.Equals(x.Instance.ServiceNode.Data.Name, nodeName, StringComparison.Ordinal));

                             if (closeSvcNode != null) {
                                 closeSvcNode.GetFromServices(serviceName).Close();
                             }

                             break;

                         case "run_svc":
                             var runSvcNode = Program.GetNodeRuntimeServices().FirstOrDefault(x => string.Equals(x.Instance.ServiceNode.Data.Name, nodeName, StringComparison.Ordinal));

                             Services[0].OneTimeExecute();

                             //node.ServiceNode.ExecuteServiceFromNodeAsync(svcName).ConfigureAwait(true);
                             break;

                         case "run_node":
                             foreach (var n in Program.GetSvcNodeIntegrations()) {
                                 nodeName = input.Split(" ")[1];

                                 if (string.Equals(n.Data.Name, nodeName, StringComparison.Ordinal)) {
                                     n.ExecuteNodeAsync(true);
                                 }
                             }
                             break;

                         default:
                             if (input.Length >= 3) 
                                 Console.WriteLine("could not find command");
                             break;
                     }

                     return exitCode = 0;
                 }
                 catch {
                     Console.WriteLine("error! missing or incorrect argument can be the cause of this restart ...\n");
                     return exitCode = 2;
                 }
             }),
        ];

        public static readonly ServiceHost[] Services = [
             new(new ServiceData("check", "check check", 1), () => {
                 byte exitCode = 0;

                 Console.WriteLine("test");

                 return exitCode;
             }),
        ];
#endif
    }
}
