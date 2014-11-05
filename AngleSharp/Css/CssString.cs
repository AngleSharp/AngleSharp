namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// Wraps a string as a CSS string value.
    /// </summary>
    sealed class CssString : ICssObject
    {
        #region Fields

        readonly String _value;

        #endregion

        #region ctor

        /// <summary>
        /// Wraps the given string.
        /// </summary>
        /// <param name="value">The value of the string.</param>
        public CssString(String value)
        {
            _value = value;
        }

        #endregion

        #region Casts

        /// <summary>
        /// Defines an explicit cast from a string to a CssString.
        /// </summary>
        /// <param name="str">The string to wrap.</param>
        /// <returns>The wrapped string.</returns>
        public static explicit operator CssString(String str)
        {
            return new CssString(str);
        }

        /// <summary>
        /// Defines an implicit cast from a CssString to a string.
        /// </summary>
        /// <param name="str">The string to unwrap.</param>
        /// <returns>The original string.</returns>
        public static implicit operator String(CssString str)
        {
            return str._value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the CSS standard represenation of the contained string.
        /// </summary>
        /// <returns>A string that contains the CSS code to create the value.</returns>
        public String ToCss()
        {
            return String.Concat("'", _value, "'");
        }

        #endregion
    }
}
