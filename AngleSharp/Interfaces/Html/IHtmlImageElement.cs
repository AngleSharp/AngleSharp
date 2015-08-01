namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the image HTML element.
    /// </summary>
    [DomName("HTMLImageElement")]
    public interface IHtmlImageElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the alternative text.
        /// </summary>
        [DomName("alt")]
        String AlternativeText { get; set; }

        /// <summary>
        /// Gets the actual used image source.
        /// </summary>
        [DomName("currentSrc")]
        String ActualSource { get; }

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        [DomName("src")]
        String Source { get; set; }

        /// <summary>
        /// Gets or sets the image candidates for higher density images.
        /// </summary>
        [DomName("srcset")]
        String SourceSet { get; set; }

        /// <summary>
        /// Gets or sets the sizes to responsively.
        /// </summary>
        [DomName("sizes")]
        String Sizes { get; set; }

        /// <summary>
        /// Gets or sets the cross-origin attribute.
        /// </summary>
        [DomName("crossOrigin")]
        String CrossOrigin { get; set; }

        /// <summary>
        /// Gets or sets the usemap attribute, which indicates that the image
        /// has an associated image map.
        /// </summary>
        [DomName("useMap")]
        String UseMap { get; set; }

        /// <summary>
        /// Gets or sets if the image element is a map. The attribute must not
        /// be specified on an element that does not have an ancestor a
        /// element with an href attribute.
        /// </summary>
        [DomName("isMap")]
        Boolean IsMap { get; set; }

        /// <summary>
        /// Gets or sets the displayed width of the image element.
        /// </summary>
        [DomName("width")]
        Int32 DisplayWidth { get; set; }

        /// <summary>
        /// Gets or sets the displayed width of the image element.
        /// </summary>
        [DomName("height")]
        Int32 DisplayHeight { get; set; }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        [DomName("naturalWidth")]
        Int32 OriginalWidth { get; }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        [DomName("naturalHeight")]
        Int32 OriginalHeight { get; }

        /// <summary>
        /// Gets if the image is completely available.
        /// </summary>
        [DomName("complete")]
        Boolean IsCompleted { get; }
    }
}
