namespace AngleSharp.Parser.Css
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom.Css;

    /// <summary>
    /// Extensions to be used exclusively by the parser or the tokenizer.
    /// </summary>
    [DebuggerStepThrough]
    static class CssParserExtensions
    {
        /// <summary>
        /// Retrieves a number describing the error of a given error code.
        /// </summary>
        /// <param name="code">A specific error code.</param>
        /// <returns>The code of the error.</returns>
        public static Int32 GetCode(this CssParseError code)
        {
            return (Int32)code;
        }

        public static Boolean Is(this CssToken token, CssTokenType a, CssTokenType b)
        {
            var type = token.Type;
            return type == a || type == b;
        }

        public static Boolean IsNot(this CssToken token, CssTokenType a, CssTokenType b)
        {
            var type = token.Type;
            return type != a && type != b;
        }

        public static Boolean IsNot(this CssToken token, CssTokenType a, CssTokenType b, CssTokenType c)
        {
            var type = token.Type;
            return type != a && type != b && type != c;
        }

        /// <summary>
        /// Checks if the provided token is actually a match token.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <returns>True if the type is matching, otherwise false.</returns>
        public static Boolean IsMatchToken(this CssToken token)
        {
            var type = token.Type;
            return type == CssTokenType.IncludeMatch ||
                type == CssTokenType.DashMatch ||
                type == CssTokenType.PrefixMatch ||
                type == CssTokenType.SubstringMatch ||
                type == CssTokenType.SuffixMatch ||
                type == CssTokenType.NotMatch;
        }
    }
}
