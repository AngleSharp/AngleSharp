using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the image element.
    /// </summary>
    [DOM("HTMLImageElement")]
    public sealed class HTMLImageElement : HTMLElement
    {
        #region Constants

        /// <summary>
        /// The img tag.
        /// </summary>
        internal const String Tag = "img";

        /// <summary>
        /// The image tag (this is not the right tag).
        /// </summary>
        internal const String FalseTag = "image";

        #endregion

        #region Members

        UInt32 _imageWidth;
        UInt32 _imageHeight;
        Boolean _loaded;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new image element.
        /// </summary>
        internal HTMLImageElement()
        {
            _loaded = true;
            _name = Tag;

            //TODO
            _imageHeight = 0;
            _imageWidth = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the image source.
        /// </summary>
        [DOM("src")]
        public String Src
        {
            get { return GetAttribute("src"); }
            set { SetAttribute("src", value); }
        }

        /// <summary>
        /// Gets or sets the alternative text.
        /// </summary>
        [DOM("alt")]
        public String Alt
        {
            get { return GetAttribute("alt"); }
            set { SetAttribute("alt", value); }
        }

        /// <summary>
        /// Gets or sets the cross-origin attribute.
        /// </summary>
        [DOM("crossOrigin")]
        public String CrossOrigin
        {
            get { return GetAttribute("crossorigin"); }
            set { SetAttribute("crossorigin", value); }
        }

        /// <summary>
        /// Gets or sets the usemap attribute, which indicates that the image has an associated image map.
        /// </summary>
        [DOM("useMap")]
        public String UseMap
        {
            get { return GetAttribute("usemap"); }
            set { SetAttribute("usemap", value); }
        }

        /// <summary>
        /// Gets or sets the displayed width of the image element.
        /// </summary>
        [DOM("width")]
        public UInt32 Width
        {
            get { return ToInteger(GetAttribute("width"), _imageWidth); }
            set { SetAttribute("width", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the displayed height of the image element.
        /// </summary>
        [DOM("height")]
        public UInt32 Height
        {
            get { return ToInteger(GetAttribute("height"), _imageHeight); }
            set { SetAttribute("height", value.ToString()); }
        }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        [DOM("naturalWidth")]
        public UInt32 NaturalWidth
        {
            get { return _imageWidth; }
        }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        [DOM("naturalHeight")]
        public UInt32 NaturalHeight
        {
            get { return _imageHeight; }
        }

        /// <summary>
        /// Gets if the image is completely available.
        /// </summary>
        [DOM("complete")]
        public Boolean Complete
        {
            get { return _loaded; }
        }

        /// <summary>
        /// Gets or sets if the image element is a map.
        /// The attribute must not be specified on an element that does not
        /// have an ancestor a element with an href attribute.
        /// </summary>
        [DOM("isMap")]
        public Boolean IsMap
        {
            get { return GetAttribute("ismap") != null; }
            set { SetAttribute("ismap", value ? string.Empty : null); }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
