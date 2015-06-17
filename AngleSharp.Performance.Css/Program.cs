namespace AngleSharp.Performance.Css
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(String[] args)
        {
            var stylesheets = new UrlTests(
                extension: ".css", 
                withBuffer: true);

            stylesheets.Include(
                ).Wait();

            var parsers = new List<ITestee>
            {
                new AngleSharpParser()
            };

            var testsuite = new TestSuite(parsers, stylesheets.Tests, new Output(), new Warmup())
            {
                NumberOfRepeats = 5,
                NumberOfReRuns = 1
            };

            testsuite.Run();
        }
    }
}
