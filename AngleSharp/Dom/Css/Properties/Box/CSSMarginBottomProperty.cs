namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin-bottom
    /// </summary>
    sealed class CssMarginBottomProperty : CssMarginPartProperty, ICssMarginBottomProperty
    {
        #region ctor

        internal CssMarginBottomProperty(CssStyleDeclaration rule)
            : base(PropertyNames.MarginBottom, rule)
        {
        }

        #endregion

        #region Properties

        public Length? Bottom
        {
            get { return Margin; }
        }

        #endregion
    }
}
