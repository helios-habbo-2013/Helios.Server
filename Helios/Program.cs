using Serilog;

namespace Helios
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Environment.Boot();
        }
    }
}