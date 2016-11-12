namespace AngleSharp.Css.Parser
{
    using System;

    /// <summary>
    /// Extensions to be used exclusively by the parser or the tokenizer.
    /// </summary>
    static class CssParseErrorExtensions
    {
        public static Int32 GetCode(this CssParseError code)
        {
            return (Int32)code;
        }
    }
}
