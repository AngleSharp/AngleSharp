namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom.Css;
    using System;

    /// <summary>
    /// Represent a stylesheet for collecting style information.
    /// </summary>
    [DomName("StyleSheet")]
    public interface IStyleSheet
    {
        /// <summary>
        /// Gets the style sheet language for this style sheet.
        /// </summary>
        [DomName("type")]
        String Type { get; }

        /// <summary>
        /// Gets the value of the attribute, which is its location. For inline
        /// style sheets, the value of this attribute is null.
        /// </summary>
        [DomName("href")]
        String Href { get; }

        /// <summary>
        /// Gets the element that associates this style sheet with the
        /// document.
        /// </summary>
        [DomName("ownerNode")]
        IElement OwnerNode { get; }

        /// <summary>
        /// Gets the parent stylesheet for style sheet languages that support
        /// the
        /// concept of style sheet inclusion.
        /// </summary>
        [DomName("parentStyleSheet")]
        IStyleSheet Parent { get; }

        /// <summary>
        /// Gets the advisory title. The title is often specified in the
        /// ownerNode.
        /// </summary>
        [DomName("title")]
        String Title { get; }

        /// <summary>
        /// Gets the intended destination media for style information. The
        /// media is often specified in the ownerNode. If no media has been
        /// specified, the MediaList is empty.
        /// </summary>
        [DomName("media")]
        [DomPutForwards("mediaText")]
        IMediaList Media { get; }

        /// <summary>
        /// Gets or sets if the stylesheet is applied to the document.
        /// Modifying this attribute may cause a new resolution of style for
        /// the document. If the media doesn't apply to the current user agent,
        /// the disabled attribute is ignored.
        /// </summary>
        [DomName("disabled")]
        Boolean IsDisabled { get; set; }
    }
}
