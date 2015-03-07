namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin-top
    /// </summary>
    sealed class CssMarginTopProperty : CssMarginPartProperty
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
