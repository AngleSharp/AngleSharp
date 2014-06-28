namespace AngleSharp.DOM.Html
{
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
        String Alt { get; set; }

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        [DomName("src")]
        String Src { get; set; }

        /// <summary>
        /// Gets or sets the cross-origin attribute.
        /// </summary>
        [DomName("crossOrigin")]
        String CrossOrigin { get; set; }

        /// <summary>
        /// Gets or sets the usemap attribute, which indicates that the image has an associated image map.
        /// </summary>
        [DomName("useMap")]
        String UseMap { get; set; }

        /// <summary>
        /// Gets or sets if the image element is a map.
        /// The attribute must not be specified on an element that does not
        /// have an ancestor a element with an href attribute.
        /// </summary>
        [DomName("isMap")]
        Boolean IsMap { get; set; }

        /// <summary>
        /// Gets or sets the displayed width of the image element.
        /// </summary>
        [DomName("width")]
        UInt32 Width { get; set; }

        /// <summary>
        /// Gets or sets the displayed width of the image element.
        /// </summary>
        [DomName("height")]
        UInt32 Height { get; set; }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        [DomName("naturalWidth")]
        UInt32 NaturalWidth { get; }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        [DomName("naturalHeight")]
        UInt32 NaturalHeight { get; }

        /// <summary>
        /// Gets if the image is completely available.
        /// </summary>
        [DomName("complete")]
        Boolean Complete { get; }
    }
}
