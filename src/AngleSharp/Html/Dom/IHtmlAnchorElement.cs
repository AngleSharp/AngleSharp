namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the a HTML element.
    /// </summary>
    [DomName("HTMLAnchorElement")]
    public interface IHtmlAnchorElement : IHtmlElement, IUrlUtilities
    {
        /// <summary>
        /// Gets or sets the name of the target frame to which the resource applies.
        /// </summary>
        [DomName("target")]
        String Target { get; set; }

        /// <summary>
        /// Gets or sets the linked resource is intended to be downloaded rather than displayed.
        /// The value represent the proposed name of the file. If the name is not a valid filename of the
        /// underlying OS, the navigator will adapt it.
        /// </summary>
        [DomName("download")]
        String Download { get; set; }

        /// <summary>
        /// Gets the element's ping attribute as a settable list of tokens.
        /// </summary>
        [DomName("ping")]
        ISettableTokenList Ping { get; }

        /// <summary>
        /// Gets or sets the rel HTML attribute, specifying the relationship
        /// of the target object to the link object.
        /// </summary>
        [DomName("rel")]
        String Relation { get; set; }

        /// <summary>
        /// Gets the rel HTML attribute, as a list of tokens.
        /// </summary>
        [DomName("relList")]
        ITokenList RelationList { get; }

        /// <summary>
        /// Gets or sets the language code for the linked resource.
        /// </summary>
        [DomName("hreflang")]
        String TargetLanguage { get; set; }

        /// <summary>
        /// Gets or sets the type of the resource. If present, the attribute must be a valid MIME type.
        /// </summary>
        [DomName("type")]
        String Type { get; }

        /// <summary>
        /// Gets the text of the anchor tag (same as TextContent).
        /// </summary>
        [DomName("text")]
        String Text { get; }
    }
}
