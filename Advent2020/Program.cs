using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Advent2020
{
    class Program
    {
        static void Main(string[] args)
        {
            var types = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass)
                .Where(t => !t.IsAbstract)
                .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IPuzzleResult<>)));

            foreach (var t in types.OrderBy(t => t.Name))
            {
                var sw = new Stopwatch();
                try
                {
                    sw.Restart();
                    dynamic instance = Activator.CreateInstance(t);
                    var result = instance.GetResult();
                    sw.Stop();

                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(t.Name);

                    Console.ResetColor();
                    Console.Write(" result: ");

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(result);

                    Console.ResetColor();
                    Console.Write(" in ");

                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($"{sw.Elapsed.TotalMilliseconds:0.##}ms");

                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{t.Name} Failed to generate ouptput with {e.GetType().Name}");

                    Console.ResetColor();
                    Console.Write(e);
                }
                finally
                {
                    Console.ResetColor();
                }
            }
        }
    }
}
