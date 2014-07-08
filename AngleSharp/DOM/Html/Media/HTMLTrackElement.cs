namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML track element.
    /// </summary>
    [DomName("HTMLTrackElement")]
    public sealed class HTMLTrackElement : HTMLElement
    {
        #region Fields

        TrackReadyState _ready;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML track element.
        /// </summary>
        internal HTMLTrackElement()
        {
            _name = Tags.Track;

            //TODO
            _ready = TrackReadyState.None;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the kind of the track.
        /// </summary>
        [DomName("kind")]
        public TrackKind Kind
        {
            get { return GetAttribute("kind").ToEnum(TrackKind.Subtitles); }
            set { SetAttribute("kind", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the media source.
        /// </summary>
        [DomName("src")]
        public String Src
        {
            get { return GetAttribute("src"); }
            set { SetAttribute("src", value); }
        }

        /// <summary>
        /// Gets or sets the language of the source.
        /// </summary>
        [DomName("srclang")]
        public String Srclang
        {
            get { return GetAttribute("srclang"); }
            set { SetAttribute("srclang", value); }
        }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        [DomName("label")]
        public String Label
        {
            get { return GetAttribute("label"); }
            set { SetAttribute("label", value); }
        }

        /// <summary>
        /// Gets or sets if given track is the default track.
        /// </summary>
        [DomName("default")]
        public Boolean Default
        {
            get { return GetAttribute("default") != null; }
            set { SetAttribute("default", value ? string.Empty : null); }
        }

        /// <summary>
        /// Gets the ready state of the given track.
        /// </summary>
        [DomName("readyState")]
        public TrackReadyState ReadyState
        {
            get { return _ready; }
        }

        /// <summary>
        /// Gets the text of the given track.
        /// </summary>
        [DomName("track")]
        public String Track
        {
            //TODO should return TextTrack
            get { return string.Empty; }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion

        #region Enumeration

        /// <summary>
        /// An enumeration with track ready state values.
        /// </summary>
        public enum TrackReadyState : ushort
        {
            /// <summary>
            /// Not initialized yet.
            /// </summary>
            None,
            /// <summary>
            /// Currently loading.
            /// </summary>
            Loading,
            /// <summary>
            /// Loading finished.
            /// </summary>
            Loaded,
            /// <summary>
            /// An error occured.
            /// </summary>
            Error
        }

        /// <summary>
        /// An enumeration with various track kinds.
        /// </summary>
        public enum TrackKind : ushort
        {
            /// <summary>
            /// A track with subtitles.
            /// </summary>
            Subtitles,
            /// <summary>
            /// A track with captions.
            /// </summary>
            Captions,
            /// <summary>
            /// A track with descriptions.
            /// </summary>
            Descriptions,
            /// <summary>
            /// A track consisting of chapters.
            /// </summary>
            Chapters,
            /// <summary>
            /// A track consisting only of metadata.
            /// </summary>
            Metadata
        }

        #endregion
    }
}
