namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/right
    /// </summary>
    sealed class CSSRightProperty : CSSCoordinateProperty, ICssRightProperty
    {
        #region ctor

        internal CSSRightProperty()
            : base(PropertyNames.Right)
        {
        }

        #endregion

        #region Property

        public IDistance Right
        {
            get { return Position; }
        }

        #endregion
    }
}
