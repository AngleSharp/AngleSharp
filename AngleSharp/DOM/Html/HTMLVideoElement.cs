using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML video element.
    /// </summary>
    public sealed class HTMLVideoElement : HTMLMediaElement
    {
        #region Constant

        /// <summary>
        /// The video tag.
        /// </summary>
        internal const string Tag = "video";

        #endregion

        #region Members

        uint videoWidth;
        uint videoHeight;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML video element.
        /// </summary>
        internal HTMLVideoElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the displayed width of the video element.
        /// </summary>
        public uint Width
        {
            get { return ToInteger(GetAttribute("width"), videoWidth); }
            set { SetAttribute("width", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the displayed height of the video element.
        /// </summary>
        public uint Height
        {
            get { return ToInteger(GetAttribute("height"), videoHeight); }
            set { SetAttribute("height", value.ToString()); }
        }

        /// <summary>
        /// Gets the width of the video.
        /// </summary>
        public uint VideoWidth
        {
            get { return videoWidth; }
        }

        /// <summary>
        /// Gets the height of the video.
        /// </summary>
        public uint VideoHeight
        {
            get { return videoHeight; }
        }

        /// <summary>
        /// Gets or sets the URL to a preview image.
        /// </summary>
        public string Poster
        {
            get { return GetAttribute("poster"); }
            set { SetAttribute("poster", value); }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return false; }
        }

        #endregion
    }
}
