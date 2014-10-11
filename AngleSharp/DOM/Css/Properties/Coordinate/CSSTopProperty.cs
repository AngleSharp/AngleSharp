namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/top
    /// </summary>
    sealed class CSSTopProperty : CSSCoordinateProperty, ICssTopProperty
    {
        #region ctor

        internal CSSTopProperty()
            : base(PropertyNames.Top)
        {
        }

        #endregion

        #region Property

        public IDistance Top
        {
            get { return Position; }
        }

        #endregion
    }
}
