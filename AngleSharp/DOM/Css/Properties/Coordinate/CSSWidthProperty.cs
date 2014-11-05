namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/width
    /// </summary>
    sealed class CSSWidthProperty : CSSCoordinateProperty, ICssWidthProperty
    {
        #region ctor

        internal CSSWidthProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Width, rule)
        {
        }

        #endregion

        #region Property

        public IDistance Width
        {
            get { return Position; }
        }

        #endregion
    }
}
