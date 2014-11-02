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
            roundOpen = new CssBracketToken { _type = CssTokenType.RoundBracketOpen };
            roundClose = new CssBracketToken { _type = CssTokenType.RoundBracketClose };
            curlyOpen = new CssBracketToken { _type = CssTokenType.CurlyBracketOpen };
            curlyClose = new CssBracketToken { _type = CssTokenType.CurlyBracketClose };
            squareOpen = new CssBracketToken { _type = CssTokenType.SquareBracketOpen };
            squareClose = new CssBracketToken { _type = CssTokenType.SquareBracketClose };
        }

        /// <summary>
        /// Creates a new CSS bracket token.
        /// </summary>
        CssBracketToken()
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

        #region String Representation

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override String ToValue()
        {
            switch (_type)
            {
                case CssTokenType.CurlyBracketOpen:
                    return "{";
                case CssTokenType.CurlyBracketClose:
                    return "}";
                case CssTokenType.RoundBracketClose:
                    return ")";
                case CssTokenType.RoundBracketOpen:
                    return "(";
                case CssTokenType.SquareBracketClose:
                    return "]";
                case CssTokenType.SquareBracketOpen:
                    return "[";
            }

            return String.Empty;
        }

        #endregion
    }
}
