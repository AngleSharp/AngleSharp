namespace AngleSharp.Parser.Xml
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Extensions to be used exclusively by the parser or the tokenizer.
    /// </summary>
    [DebuggerStepThrough]
    static class XmlParserExtensions
    {
        public static Exception Throw(this XmlParseError code)
        {
            //TODO
            return new InvalidOperationException();
        }
    }
}
