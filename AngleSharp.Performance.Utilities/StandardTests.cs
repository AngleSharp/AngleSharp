namespace AngleSharp.Performance
{
    using System;
    using System.Collections.Generic;

    public sealed class StandardTests
    {
        readonly List<ITest> _tests;

        public StandardTests()
        {
            _tests = new List<ITest>();
        }

        public List<ITest> Tests
        {
            get { return _tests; }
        }

        public StandardTests Include(params String[] values)
        {
            foreach (var value in values)
            {
                _tests.Add(new StandardTest(value));
            }

            return this;
        }
    }
}
