namespace AngleSharp.Parser.Css
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Css;
    using AngleSharp.Css.DocumentFunctions;
    using AngleSharp.Dom.Collections;

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

        /// <summary>
        /// Checks if the provided token is either of the first or the second 
        /// type of token.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <param name="a">The first type to match.</param>
        /// <param name="b">The alternative match for the token.</param>
        /// <returns>Result of the examination.</returns>
        public static Boolean Is(this CssToken token, CssTokenType a, CssTokenType b)
        {
            var type = token.Type;
            return type == a || type == b;
        }

        /// <summary>
        /// Checks if the provided token is neither of the first nor the second 
        /// type of token.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <param name="a">The first type to unmatch.</param>
        /// <param name="b">The alternative unmatch for the token.</param>
        /// <returns>Result of the examination.</returns>
        public static Boolean IsNot(this CssToken token, CssTokenType a, CssTokenType b)
        {
            var type = token.Type;
            return type != a && type != b;
        }

        /// <summary>
        /// Checks if the provided token is neither of the first, nor the
        /// second nor the third type of token.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <param name="a">The first type to unmatch.</param>
        /// <param name="b">The alternative unmatch for the token.</param>
        /// <param name="c">The final unmatch for the token.</param>
        /// <returns>Result of the examination.</returns>
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

        /// <summary>
        /// Tries to create an IDocumentFunction from the provided token.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <returns>The created IDocumentFunction or null.</returns>
        public static IDocumentFunction ToDocumentFunction(this CssToken token)
        {
            switch (token.Type)
            {
                case CssTokenType.Url:
                    return new UrlFunction(token.Data);

                case CssTokenType.UrlPrefix:
                    return new UrlPrefixFunction(token.Data);

                case CssTokenType.Domain:
                    return new DomainFunction(token.Data);

                case CssTokenType.Function:
                    if (String.Compare(token.Data, FunctionNames.Regexp, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        var str = ((CssFunctionToken)token).ToCssString();

                        if (str != null)
                            return new RegexpFunction(str);
                    }
                    break;
            }

            return null;
        }
    }
}
