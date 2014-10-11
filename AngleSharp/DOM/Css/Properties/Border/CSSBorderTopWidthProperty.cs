namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top-width
    /// </summary>
    sealed class CSSBorderTopWidthProperty : CSSBorderPartWidthProperty, ICssBorderWidthProperty
    {
        #region ctor

        internal CSSBorderTopWidthProperty()
            : base(PropertyNames.BorderTopWidth)
        {
        }

        #endregion
    }
}
