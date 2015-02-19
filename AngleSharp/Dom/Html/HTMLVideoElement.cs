namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Media;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Services.Media;
    using System;

    /// <summary>
    /// Represents the HTML video element.
    /// </summary>
    sealed class HtmlVideoElement : HTMLMediaElement<IVideoInfo>, IHtmlVideoElement
    {
        #region Fields

        readonly BoundLocation _poster;
        IVideoTrackList _videos;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML video element.
        /// </summary>
        public HtmlVideoElement(Document owner)
            : base(owner, Tags.Video)
        {
            _poster = new BoundLocation(this, AttributeNames.Poster);
            _videos = null;
        }

        #endregion

        #region Properties

        public override IVideoTrackList VideoTracks
        {
            get { return _videos; }
        }

        /// <summary>
        /// Gets or sets the displayed width of the video element.
        /// </summary>
        public Int32 DisplayWidth
        {
            get { return GetAttribute(AttributeNames.Width).ToInteger(OriginalWidth); }
            set { SetAttribute(AttributeNames.Width, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the displayed height of the video element.
        /// </summary>
        public Int32 DisplayHeight
        {
            get { return GetAttribute(AttributeNames.Height).ToInteger(OriginalHeight); }
            set { SetAttribute(AttributeNames.Height, value.ToString()); }
        }

        /// <summary>
        /// Gets the width of the video.
        /// </summary>
        public Int32 OriginalWidth
        {
            get { return _resourceTask != null ? (_resourceTask.IsCompleted && _resourceTask.Result != null ? _resourceTask.Result.Width : 0) : 0; }
        }

        /// <summary>
        /// Gets the height of the video.
        /// </summary>
        public Int32 OriginalHeight
        {
            get { return _resourceTask != null ? (_resourceTask.IsCompleted && _resourceTask.Result != null ? _resourceTask.Result.Height : 0) : 0; }
        }

        /// <summary>
        /// Gets or sets the URL to a preview image.
        /// </summary>
        public String Poster
        {
            get { return _poster.Href; }
            set { _poster.Href = value; }
        }

        #endregion
    }
}
