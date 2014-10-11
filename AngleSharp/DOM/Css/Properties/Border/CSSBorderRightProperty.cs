namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-right
    /// </summary>
    sealed class CSSBorderRightProperty : CSSBorderPartProperty, ICssBorderProperty
    {
        #region ctor

        internal CSSBorderRightProperty()
            : base(PropertyNames.BorderRight)
        {

        }

        #endregion
    }
}
