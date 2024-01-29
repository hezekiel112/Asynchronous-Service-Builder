using System.Drawing;

namespace HezekDev.ServiceBuilder {
    public static class SystemOutput {

        static Action RESET_COLOR => Console.ResetColor;
        
        public static void Disp(string message, EMessageTypeColor color) {

#if ALLOW_LOGGING
            Program.Logger(new() {
                Message = message,
            });
#endif

            FormatColor(message, (ConsoleColor) color);
            RESET_COLOR.Invoke();
        }

        public static void Disp(string message) {

#if ALLOW_LOGGING
            Program.Logger(new() {
                Message = message,
            });
#endif

            FormatColor(message, (ConsoleColor) EMessageTypeColor.ERROR_IS_RED);
            RESET_COLOR.Invoke();
        }

        static void FormatColor(string message, ConsoleColor color) {
            for (int i = 0; i >= 15; i--) {
                if (GetColors()[i] == color) {
                    Console.ForegroundColor = color;
                }
            }

            Console.WriteLine(message);
        }

        public static ConsoleColor[] GetColors() {
            return Enum.GetValues<ConsoleColor>();
        }
    }
}