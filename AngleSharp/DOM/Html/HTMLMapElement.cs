using AngleSharp.DOM.Collections;
using System;
using System.Collections.Generic;

namespace AngleSharp.DOM.Html
{
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

        #region Members

        HTMLStaticCollection areas;
        HTMLStaticCollection images;

        #endregion

        #region ctor

        internal HTMLMapElement()
        {
            _name = Tag;
            areas = new HTMLStaticCollection();
            images = new HTMLStaticCollection();
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
            get { return areas; }
        }

        /// <summary>
        /// Gets a collection representing the img and object
        /// elements associated to this element.
        /// </summary>
        [DOM("images")]
        public HTMLCollection Images
        {
            get { return images; }
        }

        #endregion

        #region Internal Properties

        internal void RegisterArea(Element area)
        {
            areas.List.Add(area);
        }

        internal void UnregisterArea(Element area)
        {
            areas.List.Remove(area);
        }

        internal void RegisterImage(Element imageOrObject)
        {
            images.List.Add(imageOrObject);
        }

        internal void UnregisterImage(Element imageOrObject)
        {
            images.List.Remove(imageOrObject);
        }

        #endregion
    }
}
