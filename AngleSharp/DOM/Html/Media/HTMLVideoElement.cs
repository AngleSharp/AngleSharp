namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML video element.
    /// </summary>
    [DomName("HTMLVideoElement")]
    public sealed class HTMLVideoElement : HTMLMediaElement
    {
        #region Fields

        UInt32 _videoWidth;
        UInt32 _videoHeight;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML video element.
        /// </summary>
        internal HTMLVideoElement()
        {
            _name = Tags.Video;

            //TODO
            _videoHeight = 0;
            _videoWidth = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the displayed width of the video element.
        /// </summary>
        [DomName("width")]
        public UInt32 Width
        {
            get { return GetAttribute("width").ToInteger(_videoWidth); }
            set { SetAttribute("width", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the displayed height of the video element.
        /// </summary>
        [DomName("height")]
        public UInt32 Height
        {
            get { return GetAttribute("height").ToInteger(_videoHeight); }
            set { SetAttribute("height", value.ToString()); }
        }

        /// <summary>
        /// Gets the width of the video.
        /// </summary>
        [DomName("videoWidth")]
        public UInt32 VideoWidth
        {
            get { return _videoWidth; }
        }

        /// <summary>
        /// Gets the height of the video.
        /// </summary>
        [DomName("videoHeight")]
        public UInt32 VideoHeight
        {
            get { return _videoHeight; }
        }

        /// <summary>
        /// Gets or sets the URL to a preview image.
        /// </summary>
        [DomName("poster")]
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
