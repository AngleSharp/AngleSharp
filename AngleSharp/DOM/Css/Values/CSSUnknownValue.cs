namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents an unknown type of value.
    /// </summary>
    sealed class CSSUnknownValue : CSSValue
    {
        public CSSUnknownValue(String text)
        {
            _type = Css.CssValueType.Custom;
            _text = text;
        }
    }
}
