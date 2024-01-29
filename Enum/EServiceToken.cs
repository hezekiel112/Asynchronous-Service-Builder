namespace HezekDev.ServiceBuilder {

    public enum EServiceToken {
        // temporary step token for checking where the setup is at
        STEP_FIRST_CALL_WANT_TO_INITIALIZE = 0x01, // the call has started the service to SERVICE_IS_INITIALIZING
        STEP_CALL_WANT_TO_RESTART = 0x02, // the call has started the service to SERVICE_IS_INITIALIZING
        STEP_LAST_CALL_WANTED_TO_END = 0x03, // the call has set the service to SERVICE_IS_ABORTED
                                             // ------------------
        SERVICE_IS_SLEEPING = 0x3, // service is alive in memory but does not respond at all
        SERVICE_IS_IDLE = 0x4, // service is alive in application and waiting to start
        SERVICE_IS_INITIALIZING = 0x5, // service is starting, currently scanning for issues
        SERVICE_IS_RUNNING = 0x6, // service is responding
        SERVICE_IS_ABORTED = 0x7, // service is aborted by application bring back to idle state when cleaning is finished
        SERVICE_IS_FAULTED = 0x8, // service has receive an exception
    }
}