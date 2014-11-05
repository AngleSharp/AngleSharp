namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML map element.
    /// </summary>
    sealed class HTMLMapElement : HTMLElement, IHtmlMapElement
    {
        #region ctor

        internal HTMLMapElement()
            : base(Tags.Map)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the href attribute.
        /// </summary>
        public String Name
        {
            get { return GetAttribute(AttributeNames.Name); }
            set { SetAttribute(AttributeNames.Name, value); }
        }

        /// <summary>
        /// Gets a collection representing the area elements
        /// associated to this map.
        /// </summary>
        public IHtmlCollection Areas
        {
            get { return new HtmlCollection<IHtmlAreaElement>(this, false); }
        }

        /// <summary>
        /// Gets a collection representing the img and object
        /// elements associated to this element.
        /// </summary>
        public IHtmlCollection Images
        {
            get { return new HtmlCollection<IHtmlImageElement>(Owner.DocumentElement, predicate: IsAssociatedImage); }
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
