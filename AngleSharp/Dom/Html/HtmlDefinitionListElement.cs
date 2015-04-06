namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML dl element.
    /// </summary>
    sealed class HtmlDefinitionListElement : HtmlElement
    {
        #region ctor

        public HtmlDefinitionListElement(Document owner, String prefix = null)
            : base(owner, Tags.Dl, prefix, NodeFlags.Special)
        {
        }

        #endregion
    }
}
