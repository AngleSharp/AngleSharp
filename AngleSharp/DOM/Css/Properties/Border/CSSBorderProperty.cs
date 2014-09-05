namespace AngleSharp.DOM.Css.Properties
{
    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border
    /// </summary>
    sealed class CSSBorderProperty : CSSBorderPartProperty, ICssBorderProperty
    {
        #region ctor

        internal CSSBorderProperty()
            : base(PropertyNames.Border)
        {
        }

        #endregion
    }
}
