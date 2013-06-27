using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML video element.
    /// </summary>
    [DOM("HTMLVideoElement")]
    public sealed class HTMLVideoElement : HTMLMediaElement
    {
        #region Constant

        /// <summary>
        /// The video tag.
        /// </summary>
        internal const String Tag = "video";

        #endregion

        #region Members

        UInt32 _videoWidth;
        UInt32 _videoHeight;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML video element.
        /// </summary>
        internal HTMLVideoElement()
        {
            _name = Tag;

            //TODO
            _videoHeight = 0;
            _videoWidth = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the displayed width of the video element.
        /// </summary>
        [DOM("width")]
        public UInt32 Width
        {
            get { return ToInteger(GetAttribute("width"), _videoWidth); }
            set { SetAttribute("width", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the displayed height of the video element.
        /// </summary>
        [DOM("height")]
        public UInt32 Height
        {
            get { return ToInteger(GetAttribute("height"), _videoHeight); }
            set { SetAttribute("height", value.ToString()); }
        }

        /// <summary>
        /// Gets the width of the video.
        /// </summary>
        [DOM("videoWidth")]
        public UInt32 VideoWidth
        {
            get { return _videoWidth; }
        }

        /// <summary>
        /// Gets the height of the video.
        /// </summary>
        [DOM("videoHeight")]
        public UInt32 VideoHeight
        {
            get { return _videoHeight; }
        }

        /// <summary>
        /// Gets or sets the URL to a preview image.
        /// </summary>
        [DOM("poster")]
        public String Poster
        {
            get { return GetAttribute("poster"); }
            set { SetAttribute("poster", value); }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return false; }
        }

        #endregion
    }
}
