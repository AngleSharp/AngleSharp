namespace AngleSharp.DOM.Css.Properties
{
    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-top
    /// </summary>
    sealed class CSSBorderTopProperty : CSSBorderPartProperty, ICssBorderProperty
    {
        #region ctor

        internal CSSBorderTopProperty()
            : base(PropertyNames.BorderTop)
        {

        }

        #endregion
    }
}
