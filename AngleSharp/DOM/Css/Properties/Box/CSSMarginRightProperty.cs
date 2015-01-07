namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin-right
    /// </summary>
    sealed class CSSMarginRightProperty : CSSMarginPartProperty, ICssMarginRightProperty
    {
        #region ctor

        internal CSSMarginRightProperty(CssStyleDeclaration rule)
            : base(PropertyNames.MarginRight, rule)
        {
        }

        #endregion

        #region Properties

        public Length? Right
        {
            get { return Margin; }
        }

        #endregion
    }
}
