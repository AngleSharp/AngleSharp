namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the source HTML element.
    /// </summary>
    [DomName("HTMLSourceElement")]
    public interface IHtmlSourceElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the URL for a media resource. Is ignored when used for
        /// the picture element.
        /// </summary>
        [DomName("src")]
        String Source { get; set; }

        /// <summary>
        /// Gets or sets the URL of a picture element.
        /// </summary>
        [DomName("srcset")]
        String SourceSet { get; set; }

        /// <summary>
        /// Gets or sets the sizes if used in conjunction with a picture.
        /// </summary>
        [DomName("sizes")]
        String Sizes { get; set; }

        /// <summary>
        /// Gets or sets the type of the media source.
        /// </summary>
        [DomName("type")]
        String Type { get; set; }

        /// <summary>
        /// Gets or sets the intended type of the media resource.
        /// </summary>
        [DomName("media")]
        String Media { get; set; }
    }
}
