using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System;

namespace Helios
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(theme: SeriLogCustomThemes.Theme,
                    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] ({SourceContext}) {Message:lj}{NewLine}{Exception}")
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            if (OperatingSystem.IsWindows())
            {
                Console.WindowHeight = (int)(Console.LargestWindowHeight * 0.95);
                Console.WindowWidth = (int)(Console.LargestWindowWidth * 0.95);
            }

            Console.WriteLine("HELIOS EMULATOR");
            Console.WriteLine("THE FLASH HABBO HOTEL EMULATOR");
            Console.WriteLine("COPYRIGHT (C) 2020-2025 BY QUACKSTER");
            Console.WriteLine("FOR MORE DETAILS CHECK LEGAL.TXT");
            Console.WriteLine();
            Console.WriteLine("BUILD");
            Console.WriteLine(" CORE: Athena, C#.NET");
            Console.WriteLine(" CLIENT: R49+");
            Console.WriteLine();

            Helios.Boot();
        }
    }
}