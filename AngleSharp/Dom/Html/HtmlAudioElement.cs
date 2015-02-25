namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Media;
    using AngleSharp.Html;
    using AngleSharp.Services.Media;

    /// <summary>
    /// Represents the HTML audio element.
    /// </summary>
    sealed class HtmlAudioElement : HTMLMediaElement<IAudioInfo>, IHtmlAudioElement
    {
        #region Fields

        IAudioTrackList _audios;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML audio element.
        /// </summary>
        public HtmlAudioElement(Document owner)
            : base(owner, Tags.Audio)
        {
            _audios = null;
        }

        #endregion

        #region Properties

        public override IAudioTrackList AudioTracks
        {
            get { return _audios; }
        }

        #endregion
    }
}
