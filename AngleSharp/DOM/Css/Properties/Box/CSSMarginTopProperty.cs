namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin-top
    /// </summary>
    sealed class CSSMarginTopProperty : CSSMarginPartProperty, ICssMarginTopProperty
    {
        #region ctor

        internal CSSMarginTopProperty()
            : base(PropertyNames.MarginTop)
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
