namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML dir element.
    /// This element is obsolete since HTML 4.01.
    /// </summary>
    [DomHistorical]
    sealed class HtmlDirectoryElement : HtmlElement
    {
        #region ctor

        public HtmlDirectoryElement(Document owner)
            : base (owner, Tags.Dir, NodeFlags.Special)
        {
        }

        #endregion
    }
}
