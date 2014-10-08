namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Media;
    using AngleSharp.Media;

    /// <summary>
    /// Represents the HTML audio element.
    /// </summary>
    sealed class HTMLAudioElement : HTMLMediaElement<IAudioInfo>, IHtmlAudioElement
    {
        #region Fields

        IAudioTrackList _audios;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML audio element.
        /// </summary>
        internal HTMLAudioElement()
            : base(Tags.Audio)
        {
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
