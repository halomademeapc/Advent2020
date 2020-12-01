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
                .Where(t => typeof(IPuzzleResult).IsAssignableFrom(t));

            foreach (var t in types)
            {
                try
                {
                    var instance = Activator.CreateInstance(t) as IPuzzleResult;
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
