namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom-width
    /// </summary>
    sealed class CSSBorderBottomWidthProperty : CSSBorderPartWidthProperty, ICssBorderWidthProperty
    {
        #region ctor

        internal CSSBorderBottomWidthProperty()
            : base(PropertyNames.BorderBottomWidth)
        {
        }

        #endregion
    }
}
