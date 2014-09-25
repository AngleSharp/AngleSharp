namespace AngleSharp.DOM.Html
{
    using AngleSharp.Media;
using System;

    /// <summary>
    /// Represents the HTML video element.
    /// </summary>
    sealed class HTMLVideoElement : HTMLMediaElement<IVideoInfo>, IHtmlVideoElement
    {
        #region Fields

        Int32 _videoWidth;
        Int32 _videoHeight;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML video element.
        /// </summary>
        internal HTMLVideoElement()
            : base(Tags.Video)
        {
            //TODO
            _videoHeight = 0;
            _videoWidth = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the displayed width of the video element.
        /// </summary>
        public Int32 DisplayWidth
        {
            get { return GetAttribute(AttributeNames.Width).ToInteger(_videoWidth); }
            set { SetAttribute(AttributeNames.Width, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the displayed height of the video element.
        /// </summary>
        public Int32 DisplayHeight
        {
            get { return GetAttribute(AttributeNames.Height).ToInteger(_videoHeight); }
            set { SetAttribute(AttributeNames.Height, value.ToString()); }
        }

        /// <summary>
        /// Gets the width of the video.
        /// </summary>
        public Int32 OriginalWidth
        {
            get { return _videoWidth; }
        }

        /// <summary>
        /// Gets the height of the video.
        /// </summary>
        public Int32 OriginalHeight
        {
            get { return _videoHeight; }
        }

        /// <summary>
        /// Gets or sets the URL to a preview image.
        /// </summary>
        public String Poster
        {
            get { return GetAttribute(AttributeNames.Poster); }
            set { SetAttribute(AttributeNames.Poster, value); }
        }

        #endregion
    }
}
