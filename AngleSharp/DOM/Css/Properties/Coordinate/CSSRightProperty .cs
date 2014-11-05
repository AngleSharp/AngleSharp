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

        internal CSSRightProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Right, rule)
        {
        }

        #endregion

        #region Property

        public IDistance Right
        {
            get { return Position; }
        }

        #endregion
    }
}
