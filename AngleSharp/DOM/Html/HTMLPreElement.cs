namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML pre element.
    /// </summary>
    sealed class HTMLPreElement : HTMLElement, IHtmlPreElement
    {
        #region ctor

        internal HTMLPreElement(String name = null)
            : base(name ?? Tags.Pre, NodeFlags.Special)
        {
        }

        #endregion
    }
}
