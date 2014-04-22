namespace AngleSharp.DOM.Css.Properties
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/width
    /// </summary>
    public sealed class CSSWidthProperty : CSSCoordinateProperty
    {
        #region ctor

        internal CSSWidthProperty()
            : base(PropertyNames.Width)
        {
        }

        #endregion
    }
}
