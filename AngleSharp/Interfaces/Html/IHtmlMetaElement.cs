namespace AngleSharp.DOM.Html
{
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
        /// Gets or sets the value of the http-equiv attribute of the meta element.
        /// </summary>
        [DomName("httpEquiv")]
        String HttpEquiv { get; set; }

        /// <summary>
        /// Gets or sets the value of the content attribute of the meta element.
        /// </summary>
        [DomName("content")]
        String Content { get; set; }
    }
}
