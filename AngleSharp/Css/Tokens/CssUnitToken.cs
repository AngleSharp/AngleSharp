using System;
using System.Globalization;

namespace AngleSharp.Css
{
    /// <summary>
    /// Represents a CSS unit token.
    /// </summary>
    sealed class CssUnitToken : CssToken
    {
        #region Members

        String _data;
        String _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS unit token.
        /// </summary>
        /// <param name="type">The exact type.</param>
        CssUnitToken(CssTokenType type)
        {
            _type = type;
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
            return new CssUnitToken(CssTokenType.Percentage) { _data = value, _unit = "%" };
        }

        /// <summary>
        /// Creates a new dimension unit token.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="dimension">The unit (dimension).</param>
        /// <returns>The created token.</returns>
        public static CssUnitToken Dimension(String value, String dimension)
        {
            return new CssUnitToken(CssTokenType.Dimension) { _data = value, _unit = dimension };
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
