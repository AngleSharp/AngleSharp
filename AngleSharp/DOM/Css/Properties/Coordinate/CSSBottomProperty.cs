namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/bottom
    /// </summary>
    sealed class CssBottomProperty : CssCoordinateProperty, ICssBottomProperty
    {
        #region ctor

        internal CssBottomProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Bottom, rule)
        {
        }

        #endregion

        #region Property

        public Length? Bottom
        {
            get { return Position; }
        }

        #endregion
    }
}
