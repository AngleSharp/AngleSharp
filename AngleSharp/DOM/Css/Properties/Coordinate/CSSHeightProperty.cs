namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/height
    /// </summary>
    sealed class CSSHeightProperty : CSSCoordinateProperty
    {
        #region ctor

        public CSSHeightProperty()
            : base(PropertyNames.Height)
        {
        }

        #endregion
    }
}
