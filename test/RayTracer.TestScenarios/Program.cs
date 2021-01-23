using System;
using System.Collections.Generic;
using RayTracer.TestScenarios.Scenarios;

namespace RayTracer.TestScenarios
{
    class Program
    {
        private static readonly Dictionary<int, (string description, IScenario runner)> _scenarios =
            new Dictionary<int, (string, IScenario)>
        {
            {
                0, ("Projectile Plotter", new ProjectilePlotter())
            },
            {
                1, ("Clock Plotter", new ClockPlotter())
            }
        };

        static void Main(string[] _)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("RayTracer Scenarios\n");
            Console.ResetColor();

            Console.WriteLine("ID - Description\n");
            foreach (var key in _scenarios.Keys)
            {
                var (description, _) = _scenarios[key];

                Console.WriteLine($"{key} - {description}");
            }

            Console.WriteLine("\nEnter the Id of the scenario: ");
            var userChoice = Console.ReadLine();
        
            if (int.TryParse(userChoice, out int choice) && _scenarios.ContainsKey(choice))
            {
                var (description, runner) = _scenarios[choice];

                Console.WriteLine($"\nRunning scenario: {description}");

                runner.Run();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nDone");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nScenario not found");
            }

            Console.ResetColor();
        }
    }
}
