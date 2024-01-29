using HezekDev.ServiceBuilder.Node;

namespace HezekDev.ServiceBuilder.Service {
    public class ServiceHost : ServiceHostModal {
        public override Func<byte> Execution {
            get;
        }

        public static void SetSleepToken(ref ServiceData data) {
            data.SetToken(EServiceToken.SERVICE_IS_SLEEPING);
        }

        public static void SetRunningToken(ref ServiceData data) {
            data.SetToken(EServiceToken.SERVICE_IS_RUNNING);
        }

        public static void SetAbortedToken(ref ServiceData data) {
            data.SetToken(EServiceToken.SERVICE_IS_ABORTED);
        }

        public override Task Restart() {
            return base.Restart();
        }

        public override Task ExecuteAsync() {
            return base.ExecuteAsync();
        }

        public override Task OneTimeExecute() {
            return base.OneTimeExecute();
        }

        public override void SetToken(ref ServiceData data, EServiceToken token) {
            base.SetToken(ref data, token);
        }

        public ServiceHost(ServiceData data, Func<byte> execution) {
            data.SetToken(EServiceToken.SERVICE_IS_IDLE);
            Data = data;
            Execution = execution;
        }

        public byte Initialize() {
            if (Data.Token == EServiceToken.SERVICE_IS_FAULTED)
                return 1;
            
            if (Program.GetNodeRuntimeServices().Where(x => x.Services.Contains(this)).Take(2).Count() == 1) {
                Data.SetToken(EServiceToken.STEP_FIRST_CALL_WANT_TO_INITIALIZE);
            }
            else return 1;

            if (Data.Token == EServiceToken.STEP_FIRST_CALL_WANT_TO_INITIALIZE) {
                if (Data.Delay == 0 || Data.Delay < short.MinValue) {
                    SystemOutput.Disp($"the service '{Data.Name}' has no delay, it will run as the default DelayOverride of 500ms\n", EMessageTypeColor.WARNING_IS_YELLOW);
                    Data.UseMin_DefaultDelay = true;
                }

                if (Data.Delay >= short.MaxValue) {
                    SystemOutput.Disp($"could not setup a custom delay for the service '{Data.Name}' because the delay value is incorrect.\nThe delay maximum value is {short.MaxValue}.\n", EMessageTypeColor.WARNING_IS_YELLOW);
                    Data.UseMax_DefaultDelay = true;
                }

                if (Data.IsOneTimeExecution) {
                    SystemOutput.Disp($"the service {Data.Name} is in one time execution.\nIt will run only when asked to and the delay is still applied but with the DelayOverride of 500ms\n");
                }

                Data.SetToken(EServiceToken.SERVICE_IS_INITIALIZING);
                SystemOutput.Disp($"service '{Data.Name}' on node '{Program.GetNodeFromService(this).Data.Name}' is initialized\n", EMessageTypeColor.SUCCESS_IS_GREEN);
            }

            return 0;
        }

        public void Close() {
            Data.SetToken(EServiceToken.STEP_LAST_CALL_WANTED_TO_END);
            try {
                for (int i = 0; Program.GetNodeRuntimeServices().Skip(i).Any(); i++) {
                    if (Program.GetNodeRuntimeServices()[i].Services.Contains(this)) {
                        Program.GetNodeRuntimeServices()[i].RemoveFromServices(this);
                        SystemOutput.Disp($"closed service : {Data.Name} - {Data.Token}\n on-going operation will still last", EMessageTypeColor.SUCCESS_IS_GREEN);
                    }
                }
            }
            catch {
                SystemOutput.Disp($"cannot close '{Data.Name}' because it does not exist in any NodeRuntimeServices", EMessageTypeColor.ERROR_IS_RED);
            }
        }

        public void AddToNode(NodeRuntimeService node) {
            node.AddIntoServices(this);
        }
    }
}