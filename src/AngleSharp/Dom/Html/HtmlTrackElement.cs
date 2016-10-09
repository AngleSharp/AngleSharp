namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Media;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML track element.
    /// </summary>
    sealed class HtmlTrackElement : HtmlElement, IHtmlTrackElement
    {
        #region Fields

        private TrackReadyState _ready;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML track element.
        /// </summary>
        public HtmlTrackElement(Document owner, String prefix = null)
            : base(owner, TagNames.Track, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
            _ready = TrackReadyState.None;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the kind of the track.
        /// </summary>
        public String Kind
        {
            get { return this.GetOwnAttribute(AttributeNames.Kind); }
            set { this.SetOwnAttribute(AttributeNames.Kind, value); }
        }

        /// <summary>
        /// Gets or sets the media source.
        /// </summary>
        public String Source
        {
            get { return this.GetUrlAttribute(AttributeNames.Src); }
            set { this.SetOwnAttribute(AttributeNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets the language of the source.
        /// </summary>
        public String SourceLanguage
        {
            get { return this.GetOwnAttribute(AttributeNames.SrcLang); }
            set { this.SetOwnAttribute(AttributeNames.SrcLang, value); }
        }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        public String Label
        {
            get { return this.GetOwnAttribute(AttributeNames.Label); }
            set { this.SetOwnAttribute(AttributeNames.Label, value); }
        }

        /// <summary>
        /// Gets or sets if given track is the default track.
        /// </summary>
        public Boolean IsDefault
        {
            get { return this.GetBoolAttribute(AttributeNames.Default); }
            set { this.SetBoolAttribute(AttributeNames.Default, value); }
        }

        /// <summary>
        /// Gets the ready state of the given track.
        /// </summary>
        public TrackReadyState ReadyState
        {
            get { return _ready; }
        }

        public ITextTrack Track
        {
            get { return null; }
        }

        #endregion
    }
}
