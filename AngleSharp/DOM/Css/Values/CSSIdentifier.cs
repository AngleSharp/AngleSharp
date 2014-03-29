namespace AngleSharp.DOM.Css
{
    using System;

    sealed class CSSIdentifier : CSSPrimitiveValue
    {
        #region ctor

        public CSSIdentifier(String token)
            : base(CssUnit.Ident)
        {
            _text = token;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        public String Identifier
        {
            get { return _text; }
        }

        #endregion
    }
}
