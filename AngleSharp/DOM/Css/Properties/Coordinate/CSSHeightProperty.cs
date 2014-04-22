namespace AngleSharp.DOM.Css.Properties
{
    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/height
    /// </summary>
    public sealed class CSSHeightProperty : CSSCoordinateProperty
    {
        #region ctor

        internal CSSHeightProperty()
            : base(PropertyNames.Height)
        {
        }

        #endregion
    }
}
