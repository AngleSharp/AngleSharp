namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the map HTML element.
    /// </summary>
    [DomName("HTMLMapElement")]
    public interface IHtmlMapElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DomName("name")]
        String Name { get; set; }

        /// <summary>
        /// Gets a collection representing the area elements
        /// associated to this map.
        /// </summary>
        [DomName("areas")]
        IHtmlCollection<IHtmlAreaElement> Areas { get; }

        /// <summary>
        /// Gets a collection representing the img and object
        /// elements associated to this element.
        /// </summary>
        [DomName("images")]
        IHtmlCollection<IHtmlImageElement> Images { get; }
    }
}
