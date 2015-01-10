namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents an HTML element with only semantic meaning.
    /// </summary>
    sealed class HTMLSemanticElement : HTMLElement
    {
        #region ctor

        public HTMLSemanticElement(Document owner, String name)
            : base(owner, name, NodeFlags.Special)
        {
        }

        #endregion
    }
}
