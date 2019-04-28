namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Media;
    using AngleSharp.Media.Dom;
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

        public override IAudioTrackList AudioTracks => _audios;

        #endregion
    }
}
