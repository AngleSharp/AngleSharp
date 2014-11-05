namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/height
    /// </summary>
    sealed class CSSHeightProperty : CSSCoordinateProperty, ICssHeightProperty
    {
        #region ctor

        internal CSSHeightProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Height, rule)
        {
        }

        #endregion

        #region Property

        public IDistance Height
        {
            get { return Position; }
        }

        #endregion
    }
}
