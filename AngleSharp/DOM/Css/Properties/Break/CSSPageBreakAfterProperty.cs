namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/page-break-after
    /// </summary>
    sealed class CSSPageBreakAfterProperty : CSSPageBreakProperty, ICssPageBreakAfterProperty
    {
        #region ctor

        internal CSSPageBreakAfterProperty()
            : base(PropertyNames.PageBreakAfter)
        {
        }

        #endregion
    }
}
