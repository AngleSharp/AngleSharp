namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the HTML map element.
    /// </summary>
    sealed class HTMLMapElement : HTMLElement, IHtmlMapElement
    {
        #region Constant

        /// <summary>
        /// The map tag.
        /// </summary>
        internal const String Tag = "map";

        #endregion

        #region Fields

        List<Element> _areas;
        List<Element> _images;

        #endregion

        #region ctor

        internal HTMLMapElement()
        {
            _name = Tag;
            _areas = new List<Element>();
            _images = new List<Element>();
            Areas = new HTMLCollection(_areas);
            Images = new HTMLCollection(_images);
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
            get;
            private set;
        }

        /// <summary>
        /// Gets a collection representing the img and object
        /// elements associated to this element.
        /// </summary>
        public IHtmlCollection Images
        {
            get;
            private set;
        }

        #endregion

        #region Internal Properties

        internal void RegisterArea(Element area)
        {
            _areas.Add(area);
        }

        internal void UnregisterArea(Element area)
        {
            _areas.Remove(area);
        }

        internal void RegisterImage(Element imageOrObject)
        {
            _images.Add(imageOrObject);
        }

        internal void UnregisterImage(Element imageOrObject)
        {
            _images.Remove(imageOrObject);
        }

        #endregion
    }
}
