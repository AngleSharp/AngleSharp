namespace AngleSharp.Performance
{
    using System;

    sealed class StandardTest : ITest
    {
        public StandardTest(String value)
            : this(value, value)
        {
        }

        public StandardTest(String name, String source)
        {
            Name = name;
            Source = source;
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
