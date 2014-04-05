namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/top
    /// </summary>
    sealed class CSSTopProperty : CSSCoordinateProperty
    {
        #region ctor

        public CSSTopProperty()
            : base(PropertyNames.Top)
        {
        }

        #endregion
    }
}
