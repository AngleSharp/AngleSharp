namespace AngleSharp.DOM.Css.Properties
{
    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/page-break-before
    /// </summary>
    sealed class CSSPageBreakBeforeProperty : CSSPageBreakProperty, ICssPageBreakBeforeProperty
    {
        #region ctor

        internal CSSPageBreakBeforeProperty()
            : base(PropertyNames.PageBreakBefore)
        {
        }

        #endregion
    }
}
