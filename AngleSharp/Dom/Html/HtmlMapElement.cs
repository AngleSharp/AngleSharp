namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML map element.
    /// </summary>
    sealed class HtmlMapElement : HtmlElement, IHtmlMapElement
    {
        #region Fields

        HtmlCollection<IHtmlAreaElement> _areas;
        HtmlCollection<IHtmlImageElement> _images;

        #endregion

        #region ctor

        public HtmlMapElement(Document owner)
            : base(owner, Tags.Map)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the href attribute.
        /// </summary>
        public String Name
        {
            get { return GetOwnAttribute(AttributeNames.Name); }
            set { SetOwnAttribute(AttributeNames.Name, value); }
        }

        /// <summary>
        /// Gets a collection representing the area elements
        /// associated to this map.
        /// </summary>
        public IHtmlCollection Areas
        {
            get { return _areas ?? (_areas = new HtmlCollection<IHtmlAreaElement>(this, false)); }
        }

        /// <summary>
        /// Gets a collection representing the img and object
        /// elements associated to this element.
        /// </summary>
        public IHtmlCollection Images
        {
            get { return _images ?? (_images = new HtmlCollection<IHtmlImageElement>(Owner.DocumentElement, predicate: IsAssociatedImage)); }
        }

        #endregion

        #region Helper

        Boolean IsAssociatedImage(IHtmlImageElement image)
        {
            var usemap = image.UseMap;

            if (!String.IsNullOrEmpty(usemap))
            {
                var name = usemap[0] == '#' ? '#' + Name : Name;
                return usemap == name;
            }

            return false;
        }

        #endregion
    }
}
