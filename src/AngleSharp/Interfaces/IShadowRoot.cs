namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// The ShadowRoot interface represents the shadow root.
    /// </summary>
    [DomName("ShadowRoot")]
    public interface IShadowRoot : IDocumentFragment
    {
        /// <summary>
        /// Gets the currently focused element in the shadow tree, if any.
        /// </summary>
        [DomName("activeElement")]
        IElement ActiveElement { get; }

        /// <summary>
        /// Gets the host element, which contains this shadow root.
        /// </summary>
        [DomName("host")]
        IElement Host { get; }

        /// <summary>
        /// Gets the markup of the current shadow root's contents.
        /// </summary>
        [DomName("innerHTML")]
        String InnerHtml { get; set; }

        /// <summary>
        /// Gets the shadow root style sheets.
        /// </summary>
        [DomName("styleSheets")]
        IStyleSheetList StyleSheets { get; }
    }
}
