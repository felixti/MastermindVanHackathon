using Microsoft.Owin.Hosting;
using System;

namespace MastermindVanHackathon
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            using (WebApp.Start<Startup>("http://localhost:8080"))
            {
                Console.WriteLine("Mastermind Server is running.");
                Console.WriteLine("Press any key to quit.");
                Console.ReadLine();
            }
        }
    }
}