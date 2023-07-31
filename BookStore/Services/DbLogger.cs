using System;

namespace BookStore.Services
{
    public class DbLogger :ILoggerService
    {
        public void Write(string message)
        {
            //Mesaj yazdırılır.
            Console.WriteLine("[DbLogger] - " + message);
        }
    }
}
