using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the image element.
    /// </summary>
    public sealed class HTMLImageElement : HTMLElement
    {
        #region Constants

        /// <summary>
        /// The img tag.
        /// </summary>
        internal const string Tag = "img";

        /// <summary>
        /// The image tag (this is not the right tag).
        /// </summary>
        internal const string FalseTag = "image";

        #endregion

        #region Members

        uint imageWidth;
        uint imageHeight;
        bool loaded;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new image element.
        /// </summary>
        internal HTMLImageElement()
        {
            loaded = true;
            _name = Tag;

            //TODO
            imageHeight = 0;
            imageWidth = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        public string Src
        {
            get { return GetAttribute("src"); }
            set { SetAttribute("src", value); }
        }

        /// <summary>
        /// Gets or sets the alternative text.
        /// </summary>
        public string Alt
        {
            get { return GetAttribute("alt"); }
            set { SetAttribute("alt", value); }
        }

        /// <summary>
        /// Gets or sets the cross-origin attribute.
        /// </summary>
        public string CrossOrigin
        {
            get { return GetAttribute("crossorigin"); }
            set { SetAttribute("crossorigin", value); }
        }

        /// <summary>
        /// Gets or sets the usemap attribute, which indicates that the image has an associated image map.
        /// </summary>
        public string UseMap
        {
            get { return GetAttribute("usemap"); }
            set { SetAttribute("usemap", value); }
        }

        /// <summary>
        /// Gets or sets the displayed width of the image element.
        /// </summary>
        public uint Width
        {
            get { return ToInteger(GetAttribute("width"), imageWidth); }
            set { SetAttribute("width", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the displayed height of the image element.
        /// </summary>
        public uint Height
        {
            get { return ToInteger(GetAttribute("height"), imageHeight); }
            set { SetAttribute("height", value.ToString()); }
        }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        public uint NaturalWidth
        {
            get { return imageWidth; }
        }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        public uint NaturalHeight
        {
            get { return imageHeight; }
        }

        /// <summary>
        /// Gets if the image is completely available.
        /// </summary>
        public bool Complete
        {
            get { return loaded; }
        }

        /// <summary>
        /// Gets or sets if the image element is a map.
        /// The attribute must not be specified on an element that does not
        /// have an ancestor a element with an href attribute.
        /// </summary>
        public bool IsMap
        {
            get { return GetAttribute("ismap") != null; }
            set { SetAttribute("ismap", value ? string.Empty : null); }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
