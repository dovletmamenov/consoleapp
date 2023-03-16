using ConsoleApp.Interfaces;

namespace ConsoleApp.Services
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
