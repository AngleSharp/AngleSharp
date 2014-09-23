namespace AngleSharp.DOM.Css.Properties
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/margin-left
    /// </summary>
    sealed class CSSMarginLeftProperty : CSSMarginPartProperty, ICssMarginLeftProperty
    {
        #region ctor

        internal CSSMarginLeftProperty()
            : base(PropertyNames.MarginLeft)
        {
        }

        #endregion

        #region Properties

        public IDistance Left
        {
            get { return Margin; }
        }

        #endregion
    }
}
