namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin-right
    /// </summary>
    sealed class CssMarginRightProperty : CssMarginPartProperty, ICssMarginRightProperty
    {
        #region ctor

        internal CssMarginRightProperty(CssStyleDeclaration rule)
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
