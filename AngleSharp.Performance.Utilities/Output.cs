namespace AngleSharp.Performance
{
    using System;

    public sealed class Output : IOutput
    {
        public void Write(String text)
        {
            Console.Write(text);
        }
    }
}
