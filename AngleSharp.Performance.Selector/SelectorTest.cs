namespace AngleSharp.Performance.Selector
{
    using System;

    sealed class SelectorTest : ITest
    {
        public SelectorTest(String selector)
        {
            Name = selector;
            Source = selector;
        }

        public String Name
        {
            get;
            private set;
        }

        public String Source
        {
            get;
            private set;
        }
    }
}
