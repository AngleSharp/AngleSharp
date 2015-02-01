namespace AngleSharp.Performance
{
    using System;

    public interface ITestee
    {
        String Name { get; }

        Type Library { get; }

        void Run(String argument);
    }
}
