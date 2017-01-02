﻿namespace AngleSharp.Html.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using System;

    /// <summary>
    /// Represents a link HTML element.
    /// </summary>
    [DomName("HTMLLinkElement")]
    public interface IHtmlLinkElement : IHtmlElement, ILinkStyle, ILinkImport, ILoadableElement
    {
        /// <summary>
        /// Gets or sets if the stylesheet is enabled or disabled.
        /// </summary>
        [DomName("disabled")]
        Boolean IsDisabled { get; set; }

        /// <summary>
        /// Gets or sets the URI for the target resource.
        /// </summary>
        [DomName("href")]
        String Href { get; set; }

        /// <summary>
        /// Gets or sets the forward relationship of the linked resource from the document to the resource.
        /// </summary>
        [DomName("rel")]
        String Relation { get; set; }

        /// <summary>
        /// Gets the list of relations contained in the rel attribute.
        /// </summary>
        [DomName("relList")]
        ITokenList RelationList { get; }

        /// <summary>
        /// Gets or sets the use with one or more target media.
        /// </summary>
        [DomName("media")]
        String Media { get; set; }

        /// <summary>
        /// Gets or sets the language code for the linked resource.
        /// </summary>
        [DomName("hreflang")]
        String TargetLanguage { get; set; }

        /// <summary>
        /// Gets or sets the content type of the style sheet language.
        /// </summary>
        [DomName("type")]
        String Type { get; set; }

        /// <summary>
        /// Gets the list of sizes defined in the sizes attribute.
        /// </summary>
        [DomName("sizes")]
        ISettableTokenList Sizes { get; }

        /// <summary>
        /// Gets or sets the linked source's integrity, if any.
        /// </summary>
        [DomName("integrity")]
        String Integrity { get; set; }

        /// <summary>
        /// Gets or sets the cross-origin attribute.
        /// </summary>
        [DomName("crossOrigin")]
        String CrossOrigin { get; set; }
    }
}
