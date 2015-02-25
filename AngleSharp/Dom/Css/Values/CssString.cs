namespace AngleSharp.Dom.Css
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

        #region Properties

        /// <summary>
        /// Gets the stored value.
        /// </summary>
        public String Value
        {
            get { return _value; }
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
