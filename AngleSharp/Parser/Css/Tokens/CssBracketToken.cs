namespace AngleSharp.Parser.Css
{
    using System;

    /// <summary>
    /// The bracket token that contains the opening or closing of a bracket.
    /// </summary>
    sealed class CssBracketToken : CssToken
    {
        #region ctor

        /// <summary>
        /// Creates a new CSS bracket token.
        /// </summary>
        CssBracketToken(CssTokenType type, String bracket, TextPosition position)
            : base(type, bracket, position)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Creates a curly bracket open token.
        /// </summary>
        public static CssBracketToken OpenRound(TextPosition position)
        {
            return new CssBracketToken(CssTokenType.RoundBracketOpen, "(", position);
        }

        /// <summary>
        /// Creates a curly bracket close token.
        /// </summary>
        public static CssBracketToken CloseRound(TextPosition position)
        {
            return new CssBracketToken(CssTokenType.RoundBracketClose, ")", position);
        }

        /// <summary>
        /// Creates a curly bracket open token.
        /// </summary>
        public static CssBracketToken OpenCurly(TextPosition position)
        {
            return new CssBracketToken(CssTokenType.CurlyBracketOpen, "{", position);
        }

        /// <summary>
        /// Creates a curly bracket close token.
        /// </summary>
        public static CssBracketToken CloseCurly(TextPosition position)
        {
            return new CssBracketToken(CssTokenType.CurlyBracketClose, "}", position);
        }

        /// <summary>
        /// Creates a square bracket open token.
        /// </summary>
        public static CssBracketToken OpenSquare(TextPosition position)
        {
            return new CssBracketToken(CssTokenType.SquareBracketOpen, "[", position);
        }

        /// <summary>
        /// Creates a square bracket close token.
        /// </summary>
        public static CssBracketToken CloseSquare(TextPosition position)
        {
            return new CssBracketToken(CssTokenType.SquareBracketClose, "]", position);
        }

        #endregion
    }
}
