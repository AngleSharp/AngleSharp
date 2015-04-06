namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents an HTML element with only semantic meaning.
    /// </summary>
    sealed class HtmlSemanticElement : HtmlElement
    {
        #region ctor

        public HtmlSemanticElement(Document owner, String name, String prefix = null)
            : base(owner, name, prefix, NodeFlags.Special)
        {
        }

        #endregion
    }
}
