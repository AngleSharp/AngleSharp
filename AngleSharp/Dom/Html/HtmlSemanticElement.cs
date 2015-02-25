namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents an HTML element with only semantic meaning.
    /// </summary>
    sealed class HtmlSemanticElement : HtmlElement
    {
        #region ctor

        public HtmlSemanticElement(Document owner, String name)
            : base(owner, name, NodeFlags.Special)
        {
        }

        #endregion
    }
}
