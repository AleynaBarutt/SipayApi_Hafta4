using System;

namespace BookStore.Services
{
    public class ConsoleLogger :ILoggerService
    {
        public void Write(string message)
        {
            //Mesaj yazdırılır.
            Console.WriteLine("[ConsoleLogger] - " + message);
        }
    }
}
