namespace AngleSharp.Parser.Css
{
    using System;

    /// <summary>
    /// The bracket token that contains the opening or closing of a bracket.
    /// </summary>
    sealed class CssBracketToken : CssToken
    {
        #region Static instances

        readonly static CssBracketToken roundOpen;
        readonly static CssBracketToken roundClose;
        readonly static CssBracketToken curlyOpen;
        readonly static CssBracketToken curlyClose;
        readonly static CssBracketToken squareOpen;
        readonly static CssBracketToken squareClose;

        #endregion

        #region ctor

        static CssBracketToken()
        {
            roundOpen = new CssBracketToken(CssTokenType.RoundBracketOpen, "(");
            roundClose = new CssBracketToken(CssTokenType.RoundBracketClose, ")");
            curlyOpen = new CssBracketToken(CssTokenType.CurlyBracketOpen, "{");
            curlyClose = new CssBracketToken(CssTokenType.CurlyBracketClose, "}");
            squareOpen = new CssBracketToken(CssTokenType.SquareBracketOpen, "[");
            squareClose = new CssBracketToken(CssTokenType.SquareBracketClose, "]");
        }

        /// <summary>
        /// Creates a new CSS bracket token.
        /// </summary>
        CssBracketToken(CssTokenType type, String bracket)
            : base(type, bracket)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a curly bracket open token.
        /// </summary>
        public static CssBracketToken OpenRound
        {
            get { return roundOpen; }
        }

        /// <summary>
        /// Gets a curly bracket close token.
        /// </summary>
        public static CssBracketToken CloseRound
        {
            get { return roundClose; }
        }

        /// <summary>
        /// Gets a curly bracket open token.
        /// </summary>
        public static CssBracketToken OpenCurly
        {
            get { return curlyOpen; }
        }

        /// <summary>
        /// Gets a curly bracket close token.
        /// </summary>
        public static CssBracketToken CloseCurly
        {
            get { return curlyClose; }
        }

        /// <summary>
        /// Gets a square bracket open token.
        /// </summary>
        public static CssBracketToken OpenSquare
        {
            get { return squareOpen; }
        }

        /// <summary>
        /// Gets a square bracket close token.
        /// </summary>
        public static CssBracketToken CloseSquare
        {
            get { return squareClose; }
        }

        #endregion
    }
}
