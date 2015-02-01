namespace AngleSharp.Performance.Selector
{
    using System;

    sealed class Output : IOutput
    {
        public void Write(String text)
        {
            Console.Write(text);
        }
    }
}
