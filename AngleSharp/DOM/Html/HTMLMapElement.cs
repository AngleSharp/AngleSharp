namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the HTML map element.
    /// </summary>
    [DOM("HTMLMapElement")]
    public sealed class HTMLMapElement : HTMLElement
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
        [DOM("name")]
        public String Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }

        /// <summary>
        /// Gets a collection representing the area elements
        /// associated to this map.
        /// </summary>
        [DOM("areas")]
        public HTMLCollection Areas
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a collection representing the img and object
        /// elements associated to this element.
        /// </summary>
        [DOM("images")]
        public HTMLCollection Images
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
