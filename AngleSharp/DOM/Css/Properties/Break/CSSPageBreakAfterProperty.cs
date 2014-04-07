namespace AngleSharp.DOM.Css.Properties
{
    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/page-break-after
    /// </summary>
    sealed class CSSPageBreakAfterProperty : CSSPageBreakProperty
    {
        #region ctor

        public CSSPageBreakAfterProperty()
            : base(PropertyNames.PageBreakAfter)
        {
            _inherited = false;
        }

        #endregion
    }
}
