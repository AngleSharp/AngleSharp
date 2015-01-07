namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/top
    /// </summary>
    sealed class CSSTopProperty : CSSCoordinateProperty, ICssTopProperty
    {
        #region ctor

        internal CSSTopProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Top, rule)
        {
        }

        #endregion

        #region Property

        public Length? Top
        {
            get { return Position; }
        }

        #endregion
    }
}
