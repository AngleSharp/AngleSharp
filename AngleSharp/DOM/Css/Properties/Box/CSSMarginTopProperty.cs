namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin-top
    /// </summary>
    sealed class CssMarginTopProperty : CssMarginPartProperty, ICssMarginTopProperty
    {
        #region ctor

        internal CssMarginTopProperty(CssStyleDeclaration rule)
            : base(PropertyNames.MarginTop, rule)
        {
        }

        #endregion

        #region Properties

        public Length? Top
        {
            get { return Margin; }
        }

        #endregion
    }
}
