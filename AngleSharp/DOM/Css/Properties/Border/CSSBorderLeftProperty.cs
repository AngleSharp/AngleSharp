namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-left
    /// </summary>
    sealed class CSSBorderLeftProperty : CSSBorderPartProperty, ICssBorderProperty
    {
        #region ctor

        internal CSSBorderLeftProperty()
            : base(PropertyNames.BorderLeft)
        {

        }

        #endregion
    }
}
