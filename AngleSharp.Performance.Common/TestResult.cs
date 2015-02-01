namespace AngleSharp.Performance
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TestResult
    {
        public TestResult(ITest test, ITestee parser)
        {
            Test = test;
            Parser = parser;
            Durations = new List<TimeSpan>();
        }

        public List<TimeSpan> Durations
        {
            get;
            private set;
        }

        public TimeSpan Shortest
        {
            get { return Durations.Count > 0 ? Durations.Min() : TimeSpan.Zero; }
        }

        public TimeSpan Longest
        {
            get { return Durations.Count > 0 ? Durations.Max() : TimeSpan.Zero; }
        }

        public TimeSpan Average
        {
            get { return TimeSpan.FromMilliseconds(Durations.Count > 0 ? Durations.Average(m => m.TotalMilliseconds) : 0.0); }
        }

        public TimeSpan Deviation
        {
            get
            {
                if (Durations.Count == 0)
                    return TimeSpan.Zero;

                var avg = Durations.Average(m => m.TotalMilliseconds);
                var sqr = Durations.Average(m => m.TotalMilliseconds * m.TotalMilliseconds);
                var dev = Math.Sqrt(sqr - avg * avg);
                return TimeSpan.FromMilliseconds(dev);
            }
        }

        public ITest Test
        {
            get;
            private set;
        }

        public ITestee Parser
        {
            get;
            private set;
        }
    }
}
