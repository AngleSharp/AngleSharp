using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Performance
{
    class TestSuite
    {
        IEnumerable<ITest> tests;
        IEnumerable<IHtmlParser> parsers;
        Dictionary<ITest, IHtmlParser> winners;
        Int32 repeats;
        Int32 reruns;

        public TestSuite()
        {
            reruns = 1;
            repeats = 10;
            winners = new Dictionary<ITest, IHtmlParser>();
            tests = Enumerable.Empty<ITest>();
            parsers = Enumerable.Empty<IHtmlParser>();
        }

        public Int32 NumberOfRepeats
        {
            get { return repeats; }
            set { repeats = value; }
        }

        public Int32 NumberOfReRuns
        {
            get { return reruns; }
            set { reruns = value; }
        }

        internal IEnumerable<ITest> Tests
        {
            get { return tests; }
            set { tests = value; }
        }

        internal IEnumerable<IHtmlParser> Parsers
        {
            get { return parsers; }
            set { parsers = value; }
        }

        public void Run()
        {
            var totalWidth = 76;
            var columns = parsers.Count() + 1;
            var widthPerColumn = totalWidth / columns;
            var sw = new Stopwatch();
            Console.WriteLine("RUNNING TESTS".Center(totalWidth));
            Console.WriteLine(String.Empty.PadRight(totalWidth, '='));
            Console.Write(String.Empty.PadLeft(widthPerColumn));

            foreach (var parser in parsers)
                Console.Write(parser.Name.Center(widthPerColumn));

            Console.WriteLine();
            Console.WriteLine(String.Empty.PadRight(totalWidth, '-'));

            foreach (var test in tests)
            {
                var source = test.Source;
                var fastest = Int64.MaxValue;

                for (int j = 0; j < reruns; j++)
                {
                    Console.Write(test.Name.Left(widthPerColumn));

                    foreach (var parser in parsers)
                    {
                        sw.Start();

                        for (int i = 0; i < repeats; i++)
                            parser.Parse(source);

                        sw.Stop();
                        var time = sw.ElapsedMilliseconds / repeats;
                        var compare = sw.ElapsedTicks;
                        Console.Write((time + "ms").Center(widthPerColumn));
                        sw.Reset();

                        if (compare < fastest)
                        {
                            winners[test] = parser;
                            fastest = compare;
                        }
                    }

                    Console.WriteLine();
                }
            }

            Console.WriteLine(String.Empty.PadRight(totalWidth, '-'));
            Console.Write("Fastest".Left(widthPerColumn));

            foreach (var parser in parsers)
                Console.Write(winners.Values.Count(m => m == parser).ToString().Center(widthPerColumn));

            Console.WriteLine();
        }
    }
}
