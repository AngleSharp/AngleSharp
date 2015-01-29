namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the area HTML element.
    /// </summary>
    [DomName("HTMLAreaElement")]
    public interface IHtmlAreaElement : IHtmlElement, IUrlUtilities
    {
        /// <summary>
        /// Gets or sets the alternative text for the element.
        /// </summary>
        [DomName("alt")]
        String AlternativeText { get; set; }

        /// <summary>
        /// Gets or sets the coordinates to define the hot-spot region.
        /// </summary>
        [DomName("coords")]
        String Coordinates { get; set; }

        /// <summary>
        /// Gets or sets the shape of the hot-spot, limited to known values.
        /// </summary>
        [DomName("shape")]
        String Shape { get; set; }

        /// <summary>
        /// Gets or sets the browsing context in which to open the linked resource.
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
        /// Gets the ping HTML attribute, as a settable list of otkens.
        /// </summary>
        [DomName("ping")]
        ISettableTokenList Ping { get; }

        /// <summary>
        /// Gets or sets the value indicating relationships of the
        /// current document to the linked resource.
        /// </summary>
        [DomName("rel")]
        String Relation { get; set; }

        /// <summary>
        /// Gets the value indicating relationships of the current
        /// document to the linked resource, as a list of tokens.
        /// </summary>
        [DomName("relList")]
        ITokenList RelationList { get; }

        /// <summary>
        /// Gets or sets the language of the linked resource.
        /// </summary>
        [DomName("hreflang")]
        String TargetLanguage { get; set; }

        /// <summary>
        /// Gets or sets the MIME type of the linked resource.
        /// </summary>
        [DomName("type")]
        String Type { get; set; }
    }
}
