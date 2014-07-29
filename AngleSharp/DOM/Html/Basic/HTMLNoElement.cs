namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents a no* HTML element (noembed, noscript, noframes).
    /// </summary>
    sealed class HTMLNoElement : HTMLElement
    {
        #region ctor

        internal HTMLNoElement(String name)
            : base(name, NodeFlags.Special)
        { }

        #endregion
    }
}
