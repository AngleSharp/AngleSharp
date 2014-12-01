namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Wraps a string as a CSS string value.
    /// </summary>
    sealed class CssString : ICssValue
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

        #region CSS Value

        CssValueType ICssValue.Type
        {
            get { return CssValueType.Primitive; }
        }

        String ICssValue.CssText
        {
            get { return _value.CssString(); }
        }

        #endregion
    }
}
