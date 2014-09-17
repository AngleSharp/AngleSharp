namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents an unknown type of value.
    /// </summary>
    sealed class CSSUnknownValue : CSSValue
    {
        #region Fields

        /// <summary>
        /// The CSS text representation of the value.
        /// </summary>
        readonly String _text;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS value.
        /// </summary>
        /// <param name="text">The text representation of the new value.</param>
        public CSSUnknownValue(String text)
            : base(CssValueType.Custom)
        {
            _text = text;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a CSS code representation of the stylesheet.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return _text;
        }

        #endregion
    }
}
