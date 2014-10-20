namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin-right
    /// </summary>
    sealed class CSSMarginRightProperty : CSSMarginPartProperty, ICssMarginRightProperty
    {
        #region ctor

        internal CSSMarginRightProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.MarginRight, rule)
        {
        }

        #endregion

        #region Properties

        public IDistance Right
        {
            get { return Margin; }
        }

        #endregion
    }
}
