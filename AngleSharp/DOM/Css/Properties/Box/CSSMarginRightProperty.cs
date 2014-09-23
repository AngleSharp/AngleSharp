namespace AngleSharp.DOM.Css.Properties
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin-right
    /// </summary>
    sealed class CSSMarginRightProperty : CSSMarginPartProperty, ICssMarginRightProperty
    {
        #region ctor

        internal CSSMarginRightProperty()
            : base(PropertyNames.MarginRight)
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
