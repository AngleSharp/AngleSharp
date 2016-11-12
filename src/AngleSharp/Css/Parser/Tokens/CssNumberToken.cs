namespace AngleSharp.Css.Parser.Tokens
{
    using System;

    /// <summary>
    /// Represents a CSS number token.
    /// </summary>
    sealed class CssNumberToken : CssToken
    {
        #region Fields

        private static readonly Char[] floatIndicators = new[] { '.', 'e', 'E' };

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS number token.
        /// </summary>
        /// <param name="number">The number to contain.</param>
        /// <param name="position">The token's position.</param>
        public CssNumberToken(String number)
            : base(CssTokenType.Number, number)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if any floating indicators are positioned.
        /// </summary>
        public Boolean IsInteger
        {
            get { return Data.IndexOfAny(floatIndicators) == -1; }
        }

        #endregion
    }
}
