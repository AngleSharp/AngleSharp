namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/right
    /// </summary>
    sealed class CSSRightProperty : CSSCoordinateProperty, ICssRightProperty
    {
        #region ctor

        internal CSSRightProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Right, rule)
        {
        }

        #endregion

        #region Property

        public Length? Right
        {
            get { return Position; }
        }

        #endregion
    }
}
