namespace AngleSharp.Dom.Html
{
    using System;
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

        public HtmlDirectoryElement(Document owner, String prefix = null)
            : base (owner, Tags.Dir, prefix, NodeFlags.Special)
        {
        }

        #endregion
    }
}
