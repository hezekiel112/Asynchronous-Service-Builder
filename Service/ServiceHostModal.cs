namespace HezekDev.ServiceBuilder.Service {
    public abstract class ServiceHostModal : IServiceHost {
        public ServiceData Data;

        byte _nodeId = 0;
        byte _restartAttempt = 0;

        public virtual void SetToken(ref ServiceData data, EServiceToken token) {
            Data.SetToken(data.Token = token);
        }


        public byte ResultKey {
            get; set;
        } = 0;

        public abstract Func<byte> Execution {
            get;
        }

        public virtual async Task Restart() {
            Data.SetToken(EServiceToken.STEP_CALL_WANT_TO_RESTART);
            await Program.GetNodeRuntimeServices()[_nodeId].ServiceNode.ExecuteServiceFromNodeAsync(Data.Name).ConfigureAwait(true);
        }

        public virtual async Task OneTimeExecute() {
            try {
                ResultKey = Execution.Invoke();

                switch (ResultKey) {
                    case 1:
                        _restartAttempt++;

                        SetToken(ref Data, EServiceToken.SERVICE_IS_ABORTED);
                        SystemOutput.Disp($"{Data.Name} has aborted!\nIf the service does not restart automaticaly, try to restart the service using 'runsvc {Data.Name}' on release mod", EMessageTypeColor.ERROR_IS_RED);
                        break;
                    case 2:
                        _restartAttempt++;

                        SetToken(ref Data, EServiceToken.SERVICE_IS_ABORTED);

                        SystemOutput.Disp($"the service '{Data.Name}' has aborted!\nservice will try to restart automaticaly.", EMessageTypeColor.ERROR_IS_RED);

                        await Restart().ConfigureAwait(true);
                        break;
                }

                await Task.Yield();
            }
            catch {
                SetToken(ref Data, EServiceToken.SERVICE_IS_FAULTED);
                SystemOutput.Disp($"{Data.Name} has stopped because an assembly error has been detected.\nif this service is the only one running, the application will exit", EMessageTypeColor.ERROR_IS_RED);
                Environment.Exit(0);
            }
        }

        public virtual async Task ExecuteAsync() {
            do {
                try {

                    if (Data.UseMin_DefaultDelay)
                        await Task.Delay(500).ConfigureAwait(true);
                    else if (Data.UseMax_DefaultDelay)
                        await Task.Delay(TimeSpan.FromSeconds(short.MaxValue)).ConfigureAwait(true);
                    else
                        await Task.Delay(TimeSpan.FromSeconds(Data.Delay)).ConfigureAwait(true);

                    for (byte i = 0; Program.GetNodeRuntimeServices().Skip(i).Any(); i++) {
                        if (Program.GetNodeRuntimeServices()[i].Services.Contains(this)) {
                            _nodeId = i;
                        }
                    }

                    ResultKey = Execution.Invoke();

#if !ALLOW_LOGGING && WATCH_DOG_START
                // as ALLOW_LOGGING permit the system to already generate informated data, this type of logging is useless
                    SystemOutput.Disp($"({Data.Token}) :: service_execution_result: ({ResultKey}) from_service: ({Data.Name}) from_node: ({Program.GetNodeRuntimeServices()[nodeId].ServiceNode.Data.Name})", EMessageTypeColor.INFO_IS_WHITE);
#endif

#if ALLOW_LOGGING
                    await Program.LoggerAsync(new() {
                        Host = Data,
                        Node = Program.GetNodeRuntimeServices()[nodeId].ServiceNode,
                        ResultKey = ResultKey,
                    }).ConfigureAwait(true);
#endif

                    switch (ResultKey) {
                        case 1:
                            _restartAttempt++;

                            SetToken(ref Data, EServiceToken.SERVICE_IS_ABORTED);
                            SystemOutput.Disp($"{Data.Name} has aborted!\nIf the service does not restart automaticaly, try to restart the service using 'runsvc {Data.Name}' on release mod", EMessageTypeColor.ERROR_IS_RED);
                            break;
                        case 2:
                            _restartAttempt++;

                            SetToken(ref Data, EServiceToken.SERVICE_IS_ABORTED);

                            SystemOutput.Disp($"the service '{Data.Name}' has aborted!\nservice will try to restart automaticaly.", EMessageTypeColor.ERROR_IS_RED);

                            await Task.Yield();
                            await Restart().ConfigureAwait(true);
                            break;
                    }
                }
                catch {
                    SetToken(ref Data, EServiceToken.SERVICE_IS_FAULTED);
                    SystemOutput.Disp($"{Data.Name} has stopped because an assembly error has been detected.\nif this service is the only one running, the application will exit", EMessageTypeColor.ERROR_IS_RED);
                    Environment.Exit(0);
                }
            }
            while (Data.Token == EServiceToken.SERVICE_IS_RUNNING);
        }
    }

}