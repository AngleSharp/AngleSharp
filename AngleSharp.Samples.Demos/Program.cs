namespace AngleSharp.Samples.Demos
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    class Program
    {
        static IEnumerable<ISnippet> FindSnippets()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var alltypes = assembly.GetTypes();
            var types = alltypes.Where(m => m.GetInterfaces().Contains(typeof(ISnippet)));
            return types.Select(m => m.GetConstructor(Type.EmptyTypes).Invoke(null) as ISnippet);
        }

        static void Main(String[] args)
        {
            var defaults = new
            {
                pause = false,
                clear = false
            };
            var snippets = FindSnippets().ToList();
            var usepause = args.Contains("--pause") || defaults.pause;
            var clearscr = args.Contains("--clear") || defaults.clear;

            RunSynchronously(async () =>
            {
                foreach (var snippet in snippets)
                {
                    Console.WriteLine(">>> {0}", snippet.GetType().Name);
                    Console.WriteLine();

                    await snippet.Run();

                    Console.WriteLine();

                    if (usepause)
                        PauseConsole();

                    if (clearscr)
                        ClearConsole();
                }
            });
        }

        static void RunSynchronously(Func<Task> runner)
        {
            runner().Wait();
        }

        static void ClearConsole()
        {
            Console.Clear();
        }

        static void PauseConsole()
        {
            Console.WriteLine("Press any key to continue ...");
            Console.ReadKey(true);
        }
    }
}
