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
        /// Checks if the provided token is part of a declaration name.
        /// </summary>
        /// <param name="token">The token to examine.</param>
        /// <returns>Result of the examination.</returns>
        public static Boolean IsDeclarationName(this CssToken token)
        {
            return token.Type != CssTokenType.EndOfFile &&
                      token.Type != CssTokenType.Colon &&
                      token.Type != CssTokenType.Whitespace &&
                      token.Type != CssTokenType.Comment &&
                      token.Type != CssTokenType.CurlyBracketOpen &&
                      token.Type != CssTokenType.Semicolon;
        }

        public static String ToText(this IEnumerable<CssToken> value)
        {
            return String.Join(String.Empty, value.Select(m => m.ToValue()));
        }
    }
}
