namespace AngleSharp.Parser.Css
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a CSS unit token.
    /// </summary>
    sealed class CssUnitToken : CssToken
    {
        #region Fields

        readonly String _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS unit token.
        /// </summary>
        /// <param name="type">The exact type.</param>
        /// <param name="value">The value.</param>
        /// <param name="dimension">The unit (dimension).</param>
        /// <param name="position">The token's position.</param>
        CssUnitToken(CssTokenType type, String value, String dimension, TextPosition position)
            : base(type, value, position)
        {
            _unit = dimension;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the contained data.
        /// </summary>
        public Single Value
        {
            get { return Single.Parse(Data, CultureInfo.InvariantCulture); }
        }

        /// <summary>
        /// Gets the contained unit.
        /// </summary>
        public String Unit
        {
            get { return _unit; }
        }

        #endregion

        #region Static Constructors

        /// <summary>
        /// Creates a new percentage unit token.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="position">The token's position.</param>
        /// <returns>The created token.</returns>
        public static CssUnitToken Percentage(String value, TextPosition position)
        {
            return new CssUnitToken(CssTokenType.Percentage, value, "%", position);
        }

        /// <summary>
        /// Creates a new dimension unit token.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="dimension">The unit (dimension).</param>
        /// <param name="position">The token's position.</param>
        /// <returns>The created token.</returns>
        public static CssUnitToken Dimension(String value, String dimension, TextPosition position)
        {
            return new CssUnitToken(CssTokenType.Dimension, value, dimension, position);
        }

        #endregion

        #region String representation

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override String ToValue()
        {
            return Data + _unit;
        }

        #endregion
    }
}
