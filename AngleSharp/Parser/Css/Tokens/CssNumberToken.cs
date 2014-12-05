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
        public CssNumberToken(String number)
            : base(CssTokenType.Number, number)
        {
        }

        public Boolean IsInteger
        {
            get { return Data.IndexOfAny(floatIndicators) == -1; }
        }

        #endregion

        #region Properties

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
