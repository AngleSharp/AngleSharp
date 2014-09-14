namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/user-ident
    /// </summary>
    sealed class CSSIdentifierValue : CSSValue
    {
        #region Fields

        readonly String _token;

        #endregion

        #region ctor

        public CSSIdentifierValue(String token)
        {
            _token = token;
            _type = CssValueType.Primitive;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        public String Value
        {
            get { return _token; }
        }

        #endregion

        #region Methods

        public override String ToCss()
        {
            return _token;
        }

        #endregion
    }
}
