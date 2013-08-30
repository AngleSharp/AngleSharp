using AngleSharp.Events;
using System;
using System.Threading.Tasks;

namespace AngleSharp
{
    interface IParser
    {
        event ParseErrorEventHandler ErrorOccurred;

        Boolean IsAsync { get; }

        void Parse();

        Task ParseAsync();
    }
}
