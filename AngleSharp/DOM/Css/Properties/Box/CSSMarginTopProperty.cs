namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin-top
    /// </summary>
    sealed class CSSMarginTopProperty : CSSMarginPartProperty, ICssMarginTopProperty
    {
        #region ctor

        internal CSSMarginTopProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.MarginTop, rule)
        {
        }

        #endregion

        #region Properties

        public IDistance Top
        {
            get { return Margin; }
        }

        #endregion
    }
}
