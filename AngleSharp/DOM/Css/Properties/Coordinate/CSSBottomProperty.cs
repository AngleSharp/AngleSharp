namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/bottom
    /// </summary>
    sealed class CSSBottomProperty : CSSCoordinateProperty, ICssBottomProperty
    {
        #region ctor

        internal CSSBottomProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Bottom, rule)
        {
        }

        #endregion

        #region Property

        public IDistance Bottom
        {
            get { return Position; }
        }

        #endregion
    }
}
