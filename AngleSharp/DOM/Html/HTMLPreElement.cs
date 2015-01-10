namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML pre element.
    /// </summary>
    sealed class HTMLPreElement : HTMLElement, IHtmlPreElement
    {
        #region ctor

        public HTMLPreElement(Document owner, String name = null)
            : base(name ?? Tags.Pre, NodeFlags.Special | NodeFlags.LineTolerance)
        {
            Owner = owner;
        }

        #endregion
    }
}
