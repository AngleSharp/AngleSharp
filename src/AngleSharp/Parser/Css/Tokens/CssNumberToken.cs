namespace AngleSharp.Parser.Css
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a CSS number token.
    /// </summary>
    sealed class CssNumberToken : CssToken
    {
        #region Fields

        static readonly Char[] floatIndicators = new[] { '.', 'e', 'E' };

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS number token.
        /// </summary>
        /// <param name="number">The number to contain.</param>
        /// <param name="position">The token's position.</param>
        public CssNumberToken(String number, TextPosition position)
            : base(CssTokenType.Number, number, position)
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

        /// <summary>
        /// Gets the contained integer value.
        /// </summary>
        public Int32 IntegerValue
        {
            get { return Int32.Parse(Data, CultureInfo.InvariantCulture); }
        }

        /// <summary>
        /// Gets the contained number.
        /// </summary>
        public Single Value
        {
            get { return Single.Parse(Data, CultureInfo.InvariantCulture); }
        }

        #endregion
    }
}
