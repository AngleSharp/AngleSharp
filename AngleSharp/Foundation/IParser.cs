using System;
using System.Threading.Tasks;

namespace AngleSharp
{
    interface IParser
    {
        event EventHandler<ParseErrorEventArgs> ErrorOccurred;

        Boolean IsAsync { get; }

        void Parse();

        Task ParseAsync();
    }
}
