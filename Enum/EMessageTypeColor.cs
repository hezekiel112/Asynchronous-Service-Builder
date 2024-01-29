using System.ComponentModel;

namespace HezekDev.ServiceBuilder {
    // the index of every EMessageTypeColor is the same of the regular ConsoleColor to ensure a similar connection to them
    public enum EMessageTypeColor {
        INFO_IS_WHITE = 15,
        WARNING_IS_YELLOW = 14,
        ERROR_IS_RED = 12,
        SUCCESS_IS_GREEN = 10,
    }
}