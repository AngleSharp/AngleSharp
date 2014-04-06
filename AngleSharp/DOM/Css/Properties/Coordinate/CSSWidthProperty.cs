namespace AngleSharp.DOM.Css.Properties
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/width
    /// </summary>
    sealed class CSSWidthProperty : CSSCoordinateProperty
    {
        #region ctor

        public CSSWidthProperty()
            : base(PropertyNames.Width)
        {
        }

        #endregion
    }
}
