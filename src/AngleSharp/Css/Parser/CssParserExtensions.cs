namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Parser.Tokens;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Extensions to be used exclusively by the parser or the tokenizer.
    /// </summary>
    static class CssParserExtensions
    {
        public static Int32 GetCode(this CssParseError code)
        {
            return (Int32)code;
        }

        public static String ToText(this IEnumerable<CssToken> value)
        {
            return String.Join(String.Empty, value.Select(m => m.ToValue()));
        }
    }
}
