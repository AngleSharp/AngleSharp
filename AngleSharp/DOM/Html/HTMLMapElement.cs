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

        readonly List<Element> _areas;
        readonly List<Element> _images;

        #endregion

        #region ctor

        internal HTMLMapElement()
        {
            _name = Tag;
            _areas = new List<Element>();
            _images = new List<Element>();
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
            get { return new HtmlElementCollection(_areas); }
        }

        /// <summary>
        /// Gets a collection representing the img and object
        /// elements associated to this element.
        /// </summary>
        public IHtmlCollection Images
        {
            get { return new HtmlElementCollection(_images); }
        }

        #endregion
    }
}
