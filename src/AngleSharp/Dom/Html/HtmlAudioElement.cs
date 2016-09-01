namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Media;
    using AngleSharp.Html;
    using AngleSharp.Services.Media;
    using System;

    /// <summary>
    /// Represents the HTML audio element.
    /// </summary>
    sealed class HtmlAudioElement : HtmlMediaElement<IAudioInfo>, IHtmlAudioElement
    {
        #region Fields

        private IAudioTrackList _audios;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML audio element.
        /// </summary>
        public HtmlAudioElement(Document owner, String prefix = null)
            : base(owner, TagNames.Audio, prefix)
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
