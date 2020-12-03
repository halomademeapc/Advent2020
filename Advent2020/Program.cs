using System;
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

            foreach (var t in types)
            {
                try
                {
                    dynamic instance = Activator.CreateInstance(t);
                    var result = instance.GetResult();
                    Console.WriteLine($"{t.Name} result: {result}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{t.Name} Failed to generate ouptput with {e.GetType().Name}");
                    Console.Write(e);
                }
            }
        }
    }
}
