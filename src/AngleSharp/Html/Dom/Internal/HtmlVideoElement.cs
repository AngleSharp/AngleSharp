namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Media;
    using AngleSharp.Media.Dom;
    using AngleSharp.Text;
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

        public override IVideoTrackList VideoTracks => _videos;

        public Int32 DisplayWidth
        {
            get => this.GetOwnAttribute(AttributeNames.Width).ToInteger(OriginalWidth);
            set => this.SetOwnAttribute(AttributeNames.Width, value.ToString());
        }

        public Int32 DisplayHeight
        {
            get => this.GetOwnAttribute(AttributeNames.Height).ToInteger(OriginalHeight);
            set => this.SetOwnAttribute(AttributeNames.Height, value.ToString());
        }

        public Int32 OriginalWidth => Media?.Width ?? 0;

        public Int32 OriginalHeight => Media?.Height ?? 0;

        public String Poster
        {
            get => this.GetUrlAttribute(AttributeNames.Poster);
            set => this.SetOwnAttribute(AttributeNames.Poster, value);
        }

        #endregion
    }
}
