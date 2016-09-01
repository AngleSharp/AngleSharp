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
    sealed class HtmlVideoElement : HtmlMediaElement<IVideoInfo>, IHtmlVideoElement
    {
        #region Fields

        private IVideoTrackList _videos;

        #endregion

        #region ctor

        public HtmlVideoElement(Document owner, String prefix = null)
            : base(owner, TagNames.Video, prefix)
        {
            _videos = null;
        }

        #endregion

        #region Properties

        public override IVideoTrackList VideoTracks
        {
            get { return _videos; }
        }

        public Int32 DisplayWidth
        {
            get { return this.GetOwnAttribute(AttributeNames.Width).ToInteger(OriginalWidth); }
            set { this.SetOwnAttribute(AttributeNames.Width, value.ToString()); }
        }

        public Int32 DisplayHeight
        {
            get { return this.GetOwnAttribute(AttributeNames.Height).ToInteger(OriginalHeight); }
            set { this.SetOwnAttribute(AttributeNames.Height, value.ToString()); }
        }

        public Int32 OriginalWidth
        {
            get { return Media?.Width ?? 0; }
        }

        public Int32 OriginalHeight
        {
            get { return Media?.Height ?? 0; }
        }

        public String Poster
        {
            get { return this.GetUrlAttribute(AttributeNames.Poster); }
            set { this.SetOwnAttribute(AttributeNames.Poster, value); }
        }

        #endregion
    }
}
