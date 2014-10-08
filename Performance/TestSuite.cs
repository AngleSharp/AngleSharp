namespace Performance
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    class TestSuite
    {
        readonly List<TestResult> _results;
        readonly IEnumerable<ITest> _tests;
        readonly IEnumerable<IHtmlParser> _parsers;

        Int32 _repeats;
        Int32 _reruns;

        public TestSuite(IEnumerable<IHtmlParser> parsers, IEnumerable<ITest> tests)
        {
            _reruns = 1;
            _repeats = 10;
            _tests = tests;
            _parsers = parsers;
            _results = new List<TestResult>(parsers.Join(tests, m => 0, m => 0, (a, b) => new TestResult(b, a)));
        }

        public Int32 NumberOfRepeats
        {
            get { return _repeats; }
            set { _repeats = value; }
        }

        public Int32 NumberOfReRuns
        {
            get { return _reruns; }
            set { _reruns = value; }
        }

        internal IEnumerable<ITest> Tests
        {
            get { return _tests; }
        }

        internal IEnumerable<IHtmlParser> Parsers
        {
            get { return _parsers; }
        }

        public void Run()
        {
            var totalWidth = 76;
            var columns = _parsers.Count() + 1;
            var widthPerColumn = totalWidth / columns;
            var sw = new Stopwatch();
            Console.WriteLine("RUNNING TESTS".Center(totalWidth));
            Console.WriteLine(String.Empty.PadRight(totalWidth, '='));
            Console.Write(String.Empty.PadLeft(widthPerColumn));

            foreach (var parser in _parsers)
                Console.Write(parser.Name.Center(widthPerColumn));

            Console.WriteLine();
            Console.WriteLine(String.Empty.PadRight(totalWidth, '-'));

            foreach (var test in _tests)
            {
                if (test == null)
                    continue;

                var source = test.Source;

                for (int j = 0; j < _reruns; j++)
                {
                    Console.Write(test.Name.Left(widthPerColumn));

                    foreach (var parser in _parsers)
                    {
                        var result = _results.First(m => m.Parser == parser && m.Test == test);
                        sw.Restart();

                        for (int i = 0; i < _repeats; i++)
                            parser.Parse(source);

                        sw.Stop();

                        result.Durations.Add(sw.Elapsed);
                        Console.Write(((sw.ElapsedMilliseconds / _repeats) + "ms").Center(widthPerColumn));
                    }

                    Console.WriteLine();
                }
            }

            Console.WriteLine(String.Empty.PadRight(totalWidth, '-'));
            Console.Write("Fastest".Left(widthPerColumn));

            foreach (var parser in _parsers)
            {
                var winner = _tests.Count(test => _results.Where(m => m.Test == test).OrderBy(m => m.Shortest).Select(m => m.Parser).First() == parser);
                Console.Write(winner.ToString().Center(widthPerColumn));
            }

            Console.WriteLine();
        }
    }
}
