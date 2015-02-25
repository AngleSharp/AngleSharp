namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin-left
    /// </summary>
    sealed class CssMarginLeftProperty : CssMarginPartProperty, ICssMarginLeftProperty
    {
        #region ctor

        internal CssMarginLeftProperty(CssStyleDeclaration rule)
            : base(PropertyNames.MarginLeft, rule)
        {
        }

        #endregion

        #region Properties

        public Length? Left
        {
            get { return Margin; }
        }

        #endregion
    }
}
