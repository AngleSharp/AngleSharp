namespace AngleSharp.DOM.Css
{
    using System;

    sealed class CSSIdent : CSSValue
    {
        public CSSIdent(String token)
        {
            _type = Css.CssValueType.PrimitiveValue;
            _text = token;
        }

        /// <summary>
        /// Gets the token of the CSS identifier.
        /// </summary>
        public String Token
        {
            get { return _text; }
        }
    }
}
