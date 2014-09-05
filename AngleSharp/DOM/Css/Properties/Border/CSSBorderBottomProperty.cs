namespace AngleSharp.DOM.Css.Properties
{
    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-bottom
    /// </summary>
    sealed class CSSBorderBottomProperty : CSSBorderPartProperty, ICssBorderProperty
    {
        #region ctor

        internal CSSBorderBottomProperty()
            : base(PropertyNames.BorderBottom)
        {

        }

        #endregion
    }
}
