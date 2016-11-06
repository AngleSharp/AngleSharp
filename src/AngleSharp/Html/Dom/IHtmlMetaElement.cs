namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the meta HTML element.
    /// </summary>
    [DomName("HTMLMetaElement")]
    public interface IHtmlMetaElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the name of the meta element.
        /// </summary>
        [DomName("name")]
        String Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the equivalent in a meta element, which
        /// is effective if the server doesn't send a corresponding real header.
        /// </summary>
        [DomName("httpEquiv")]
        String HttpEquivalent { get; set; }

        /// <summary>
        /// Gets or sets the value of the content attribute of the meta element.
        /// </summary>
        [DomName("content")]
        String Content { get; set; }
    }
}
