using System;

namespace Monad
{
    public interface ILogger
    {
        void Log(string log);
    }

    public class ConsoleLogger : ILogger
    {
        public void Log(string log)
        {
            Console.WriteLine(log);
        }
    }
}