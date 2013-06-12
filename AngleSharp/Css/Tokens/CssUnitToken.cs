using System;

namespace AngleSharp.Css
{
    /// <summary>
    /// Represents a CSS unit token.
    /// </summary>
    sealed class CssUnitToken : CssToken
    {
        #region Members

        float _data;
        string _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS unit token.
        /// </summary>
        /// <param name="type">The exact type.</param>
        private CssUnitToken(CssTokenType type)
        {
            _type = type;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the contained data.
        /// </summary>
        public float Data
        {
            get { return _data; }
            set { _data = value; }
        }

        /// <summary>
        /// Gets the contained unit.
        /// </summary>
        public string Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        #endregion

        #region Static Constructors

        /// <summary>
        /// Creates a new percentage unit token.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The created token.</returns>
        public static CssUnitToken Percentage(float value)
        {
            return new CssUnitToken(CssTokenType.Percentage) { _data = value, _unit = "%" };
        }

        /// <summary>
        /// Creates a new dimension unit token.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="dimension">The unit (dimension).</param>
        /// <returns>The created token.</returns>
        public static CssUnitToken Dimension(float value, string dimension)
        {
            return new CssUnitToken(CssTokenType.Dimension) { _data = value, _unit = dimension };
        }

        #endregion

        #region string representation

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override string ToValue()
        {
            return _data.ToString() + _unit;
        }

        #endregion
    }
}
