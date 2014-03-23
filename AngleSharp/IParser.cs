namespace AngleSharp
{
    using AngleSharp.Events;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Definition for any parser in the AngleSharp library.
    /// </summary>
    interface IParser
    {
        /// <summary>
        /// This event is raised when an error occurred.
        /// </summary>
        event EventHandler<ParseErrorEventArgs> ParseError;

        /// <summary>
        /// Determines if the parser is working async.
        /// </summary>
        Boolean IsAsync { get; }

        /// <summary>
        /// Parses the given content synchronously.
        /// </summary>
        void Parse();

        /// <summary>
        /// Parses the given content asynchronously.
        /// </summary>
        /// <returns>The task that determines if the parsing process has completed.</returns>
        Task ParseAsync();
    }
}
