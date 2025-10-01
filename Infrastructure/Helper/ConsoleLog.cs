using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Helper
{
    public static class ConsoleLog
    {
        public static string NL = Environment.NewLine; // shortcut
        public static string NORMAL =  "\x1b[39m";
        public static string RED =  "\x1b[91m";
        public static string GREEN =  "\x1b[92m";
        public static string YELLOW =  "\x1b[93m";
        public static string BLUE =  "\x1b[94m";
        public static string MAGENTA =  "\x1b[95m";
        public static string CYAN =  "\x1b[96m";
        public static string GREY =  "\x1b[97m";
        public static string BOLD =  "\x1b[1m";
        public static string NOBOLD =  "\x1b[22m";
        public static string UNDERLINE =  "\x1b[4m";
        public static string NOUNDERLINE =  "\x1b[24m";
        public static string REVERSE =  "\x1b[7m";
        public static string NOREVERSE =  "\x1b[27m";

        public static string RED_REPLATE = "[RED]";
        public static string GREEN_REPLATE = "[GREEN]";
        public static string YELLOW_REPLATE = "[YELLOW]";
        public static string BLUE_REPLATE = "[BLUE]";
        public static string MAGENTA_REPLATE = "[MAGENTA]";
        public static string CYAN_REPLATE = "[CYAN]";
        public static string GREY_REPLATE = "[GREY]";
        public static string BOLD_REPLATE = "[BOLD]";
        public static string NOBOLD_REPLATE = "[NOBOLD]";
        public static string UNDERLINE_REPLATE = "[UNDERLINE]";
        public static string NOUNDERLINE_REPLATE = "[NOUNDERLINE]";
        public static string REVERSE_REPLATE = "[REVERSE]";
        public static string NOREVERSE_REPLATE = "[NOREVERSE]";
        public static string NORMAL_REPLATE = "[NORMAL]";


        public static int SUCCESS = 0;
        public static int WARNING = 1;
        public static int ERROR = 2;
        public static int INFO = 3;
        public static void Log(string message)
        {
            string mes = message.Replace("\n", NL)
                .Replace("[RED]",ConsoleLog.RED)
                .Replace("[GREEN]", ConsoleLog.GREEN)
                .Replace("[YELLOW]", ConsoleLog.YELLOW)
                .Replace("[BLUE]", ConsoleLog.BLUE)
                .Replace("[MAGENTA]", ConsoleLog.MAGENTA)
                .Replace("[CYAN]", ConsoleLog.CYAN)
                .Replace("[GREY]", ConsoleLog.GREY)
                .Replace("[BOLD]", ConsoleLog.BOLD)
                .Replace("[NOBOLD]", ConsoleLog.NOBOLD)
                .Replace("[UNDERLINE]", ConsoleLog.UNDERLINE)
                .Replace("[NOUNDERLINE]", ConsoleLog.NOUNDERLINE)
                .Replace("[REVERSE]", ConsoleLog.REVERSE)
                .Replace("[NOREVERSE]", ConsoleLog.NOREVERSE)
                .Replace("[NORMAL]", ConsoleLog.NORMAL);
            Console.WriteLine($"{mes}");
        }
        public static void Log(int Status,string className, string message, string time = null )
        {
            string timeM = time ?? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string statusMes = Status == SUCCESS ? "[GREEN]SUCCESS[NORMAL]" : Status == WARNING ? "[YELLOW]WARNING[NORMAL]" : Status == ERROR ? "[RED]ERROR[NORMAL]" : "[BLUE]INFO[NORMAL]";
            string logMess = $"[YELLOW]{timeM}[NORMAL] - {statusMes} - {message} ";
            Log(logMess);
        }
    }
}
