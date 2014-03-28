namespace AngleSharp.DOM.Css
{
    using System;

    sealed class CSSIdentifier : CSSValue
    {
        #region ctor

        public CSSIdentifier(String token)
        {
            _type = CssValueType.PrimitiveValue;
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

        /// <summary>
        /// Gets the unit type of the value.
        /// </summary>
        public CssUnit PrimitiveType
        {
            get { return CssUnit.Ident; }
        }

        #endregion
    }
}
