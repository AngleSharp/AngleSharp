namespace AngleSharp.Parser.Css
{
    using System;
    using System.Globalization;
    using AngleSharp.Dom.Css;

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

        #region Methods

        /// <summary>
        /// Converts the number to a value. Uses an allocated value for the 0.
        /// </summary>
        /// <returns>The created value.</returns>
        public Number ToNumber()
        {
            var value = Value;

            if (value == 0f)
                return Number.Zero;

            var unit = IsInteger ? Number.Unit.Integer : Number.Unit.Float;
            return new Number(value, unit);
        }

        #endregion
    }
}
