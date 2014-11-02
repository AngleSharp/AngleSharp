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

        readonly String _data;
        readonly String _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS unit token.
        /// </summary>
        /// <param name="type">The exact type.</param>
        /// <param name="value">The value.</param>
        /// <param name="dimension">The unit (dimension).</param>
        CssUnitToken(CssTokenType type, String value, String dimension)
        {
            _type = type;
            _data = value;
            _unit = dimension;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the contained data.
        /// </summary>
        public Single Data
        {
            get { return Single.Parse(_data, CultureInfo.InvariantCulture); }
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
        /// <returns>The created token.</returns>
        public static CssUnitToken Percentage(String value)
        {
            return new CssUnitToken(CssTokenType.Percentage, value, "%");
        }

        /// <summary>
        /// Creates a new dimension unit token.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="dimension">The unit (dimension).</param>
        /// <returns>The created token.</returns>
        public static CssUnitToken Dimension(String value, String dimension)
        {
            return new CssUnitToken(CssTokenType.Dimension, value, dimension);
        }

        #endregion

        #region String representation

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override String ToValue()
        {
            return _data + _unit;
        }

        #endregion
    }
}
