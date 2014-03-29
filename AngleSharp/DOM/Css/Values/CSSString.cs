namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents an string in CSS.
    /// </summary>
    sealed class CSSString : CSSPrimitiveValue
    {
        #region Fields

        String _value;

        #endregion

        #region ctor

        public CSSString(String value)
            : base(CssUnit.String)
        {
            _text = String.Concat("'", value, "'");
            _value = value;

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value.
        /// </summary>
        public String Value
        {
            get { return _value; }
        }

        #endregion
    }
}
