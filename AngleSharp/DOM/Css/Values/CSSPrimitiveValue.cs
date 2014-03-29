namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a CSS primitive value.
    /// </summary>
    abstract class CSSPrimitiveValue : CSSValue
    {
        #region Fields

        CssUnit _unit;

        #endregion

        #region ctor

        public CSSPrimitiveValue(CssUnit unit)
        {
            _type = CssValueType.PrimitiveValue;
            _unit = unit;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the unit type of the value.
        /// </summary>
        public CssUnit PrimitiveType
        {
            get { return _unit; }
        }

        #endregion
    }
}
