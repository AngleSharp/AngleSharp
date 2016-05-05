namespace AngleSharp.Performance
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class TestSuite
    {
        readonly List<TestResult> _results;
        readonly IEnumerable<ITest> _tests;
        readonly IEnumerable<ITestee> _parsers;
        readonly IOutput _output;
        readonly IWarmup _warmup;

        Int32 _repeats;
        Int32 _reruns;

        public TestSuite(IEnumerable<ITestee> parsers, IEnumerable<ITest> tests, IOutput output, IWarmup warmup = null)
        {
            _reruns = 1;
            _repeats = 10;
            _parsers = parsers;
            _tests = tests;
            _output = output;
            _warmup = warmup;
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

        internal IEnumerable<ITestee> Parsers
        {
            get { return _parsers; }
        }

        internal IOutput Console
        {
            get { return _output; }
        }

        void Warmup()
        {
            if (_warmup == null)
                return;

            foreach (var testee in _parsers)
                _warmup.ForceJit(testee.Library);
        }

        public void Run()
        {
            Warmup();

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
                            parser.Run(source);

                        sw.Stop();

                        result.Durations.Add(sw.Elapsed);
                        Console.Write(((sw.ElapsedMilliseconds / _repeats) + "ms").Center(widthPerColumn));
                    }

                    Console.WriteLine();
                }
            }

            Console.WriteLine(String.Empty.PadRight(totalWidth, '-'));
            Console.Write("Total".Left(widthPerColumn));

            foreach (var parser in _parsers)
            {
                var total = _results.Where(m => m.Parser == parser).Sum(m => m.Shortest.TotalMilliseconds);
                Console.Write((Math.Truncate(total / _repeats)  + "ms").Center(widthPerColumn));
            }

            Console.WriteLine();
            Console.WriteLine(String.Empty.PadRight(totalWidth, '-'));
            Console.Write("Fastest".Left(widthPerColumn));

            foreach (var parser in _parsers)
            {
                var winner = _tests.Count(test => _results.Where(m => m.Test == test).OrderBy(m => m.Shortest).Select(m => m.Parser).First() == parser);
                Console.Write(winner.ToString().Center(widthPerColumn));
            }

            Console.WriteLine();
            Console.WriteLine(String.Empty.PadRight(totalWidth, '-'));
            Console.Write("Slowest".Left(widthPerColumn));

            foreach (var parser in _parsers)
            {
                var loser = _tests.Count(test => _results.Where(m => m.Test == test).OrderBy(m => m.Shortest).Select(m => m.Parser).Last() == parser);
                Console.Write(loser.ToString().Center(widthPerColumn));
            }

            Console.WriteLine();
            Console.WriteLine(String.Empty.PadRight(totalWidth, '-'));
            Console.WriteLine();
        }
    }
}
