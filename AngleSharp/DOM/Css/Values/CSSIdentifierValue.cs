namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/user-ident
    /// </summary>
    sealed class CSSIdentifierValue : CSSPrimitiveValue
    {
        #region ctor

        public CSSIdentifierValue(String token)
        {
            _text = token;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        public String Value
        {
            get { return _text; }
        }

        #endregion
    }
}
