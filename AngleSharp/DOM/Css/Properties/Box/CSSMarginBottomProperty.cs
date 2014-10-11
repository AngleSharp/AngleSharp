namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin-bottom
    /// </summary>
    sealed class CSSMarginBottomProperty : CSSMarginPartProperty, ICssMarginBottomProperty
    {
        #region ctor

        internal CSSMarginBottomProperty()
            : base(PropertyNames.MarginBottom)
        {
        }

        #endregion

        #region Properties

        public IDistance Bottom
        {
            get { return Margin; }
        }

        #endregion
    }
}
