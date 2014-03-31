namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a CSS primitive value.
    /// </summary>
    abstract class CSSPrimitiveValue : CSSValue
    {
        #region ctor

        public CSSPrimitiveValue()
        {
            _type = CssValueType.PrimitiveValue;
        }

        #endregion
    }
}
