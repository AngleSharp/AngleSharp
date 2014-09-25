namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Events;
    using AngleSharp.DOM.Media;
    using AngleSharp.Media;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the abstract base for HTML media (audio / video) elements.
    /// </summary>
    abstract class HTMLMediaElement<TResource> : HTMLElement, IHtmlMediaElement
        where TResource : IResourceInfo
    {
        #region Fields

        /// <summary>
        /// The source url.
        /// </summary>
        protected String _source;
        /// <summary>
        /// The state of the network.
        /// </summary>
        protected MediaNetworkState _network;
        /// <summary>
        /// The state of the network.
        /// </summary>
        protected IMediaController _controller;
        /// <summary>
        /// Currently seeking ?
        /// </summary>
        protected Boolean _seeking;
        /// <summary>
        /// The total time (duration).
        /// </summary>
        protected Double _duration;
        /// <summary>
        /// The current time.
        /// </summary>
        protected Double _currentTime;
        /// <summary>
        /// Currently muted ?
        /// </summary>
        protected Boolean? _muted;
        /// <summary>
        /// The volume.
        /// </summary>
        protected Double _volume;
        /// <summary>
        /// The task that loads the resource.
        /// </summary>
        protected Task<TResource> _resourceTask;

        IAudioTrackList _audios;
        IVideoTrackList _videos;
        ITextTrackList _texts;

        #endregion

        #region Events

        public event DomEventHandler Emptied
        {
            add { AddEventListener(EventNames.Emptied, value); }
            remove { RemoveEventListener(EventNames.Emptied, value); }
        }

        public event DomEventHandler LoadedMetadata
        {
            add { AddEventListener(EventNames.LoadedMetaData, value); }
            remove { RemoveEventListener(EventNames.LoadedMetaData, value); }
        }

        public event DomEventHandler LoadedData
        {
            add { AddEventListener(EventNames.LoadedData, value); }
            remove { RemoveEventListener(EventNames.LoadedData, value); }
        }

        public event DomEventHandler CanPlay
        {
            add { AddEventListener(EventNames.CanPlay, value); }
            remove { RemoveEventListener(EventNames.CanPlay, value); }
        }

        public event DomEventHandler CanPlayThrough
        {
            add { AddEventListener(EventNames.CanPlayThrough, value); }
            remove { RemoveEventListener(EventNames.CanPlayThrough, value); }
        }

        public event DomEventHandler Ended
        {
            add { AddEventListener(EventNames.Ended, value); }
            remove { RemoveEventListener(EventNames.Ended, value); }
        }

        public event DomEventHandler Waiting
        {
            add { AddEventListener(EventNames.Waiting, value); }
            remove { RemoveEventListener(EventNames.Waiting, value); }
        }

        public event DomEventHandler DurationChanged
        {
            add { AddEventListener(EventNames.DurationChange, value); }
            remove { RemoveEventListener(EventNames.DurationChange, value); }
        }

        public event DomEventHandler TimeUpdated
        {
            add { AddEventListener(EventNames.TimeUpdate, value); }
            remove { RemoveEventListener(EventNames.TimeUpdate, value); }
        }

        public event DomEventHandler Paused
        {
            add { AddEventListener(EventNames.Pause, value); }
            remove { RemoveEventListener(EventNames.Pause, value); }
        }

        public event DomEventHandler Played
        {
            add { AddEventListener(EventNames.Play, value); }
            remove { RemoveEventListener(EventNames.Play, value); }
        }

        public event DomEventHandler Playing
        {
            add { AddEventListener(EventNames.Playing, value); }
            remove { RemoveEventListener(EventNames.Playing, value); }
        }

        public event DomEventHandler RateChanged
        {
            add { AddEventListener(EventNames.RateChange, value); }
            remove { RemoveEventListener(EventNames.RateChange, value); }
        }

        public event DomEventHandler VolumeChanged
        {
            add { AddEventListener(EventNames.VolumeChange, value); }
            remove { RemoveEventListener(EventNames.VolumeChange, value); }
        }

        #endregion

        #region ctor

        internal HTMLMediaElement(String name)
            : base(name)
        {
            _volume = 1.0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the media source.
        /// </summary>
        public String Source
        {
            get { return GetAttribute(AttributeNames.Src); }
            set { SetAttribute(AttributeNames.Src, value); }
        }

        /// <summary>
        /// Gets or sets the cross-origin attribute.
        /// </summary>
        public String CrossOrigin
        {
            get { return GetAttribute(AttributeNames.CrossOrigin); }
            set { SetAttribute(AttributeNames.CrossOrigin, value); }
        }

        /// <summary>
        /// Gets or sets the preload attribute.
        /// </summary>
        public String Preload
        {
            get { return GetAttribute(AttributeNames.Preload); }
            set { SetAttribute(AttributeNames.Preload, value); }
        }

        /// <summary>
        /// Gets the current network state.
        /// </summary>
        public MediaNetworkState NetworkState
        {
            get { return _network; }
        }

        /// <summary>
        /// Gets the current ready state.
        /// </summary>
        public MediaReadyState ReadyState
        {
            get { return _controller == null ? MediaReadyState.Nothing : _controller.ReadyState; }
        }

        /// <summary>
        /// Gets if seeking is currently active.
        /// </summary>
        public Boolean IsSeeking
        {
            get { return _seeking; }
        }

        /// <summary>
        /// Gets the current media source.
        /// </summary>
        public String CurrentSource
        {
            get { return _source; }
        }

        /// <summary>
        /// Gets the time in seconds.
        /// </summary>
        public Double Duration
        {
            get { return _duration; }
        }

        /// <summary>
        /// Gets or sets the current time in seconds.
        /// </summary>
        public Double CurrentTime
        {
            get { return _currentTime; }
            set
            {
                if (value < 0)
                    _currentTime = 0;
                else if (value > Duration)
                    _currentTime = Duration;
                else
                    _currentTime = value;

                var ev = new Event();
                ev.Init(EventNames.DurationChange, true, true);
                Dispatch(ev);
            }
        }

        public Boolean IsAutoplay
        {
            get { return GetAttribute(AttributeNames.Autoplay) != null; }
            set { SetAttribute(AttributeNames.Autoplay, value ? String.Empty : null); }
        }

        public Boolean IsLoop
        {
            get { return GetAttribute(AttributeNames.Loop) != null; }
            set { SetAttribute(AttributeNames.Loop, value ? String.Empty : null); }
        }

        public Boolean IsShowingControls
        {
            get { return GetAttribute(AttributeNames.Controls) != null; }
            set { SetAttribute(AttributeNames.Controls, value ? String.Empty : null); }
        }

        public Boolean IsDefaultMuted
        {
            get { return GetAttribute(AttributeNames.Muted) != null; }
            set { SetAttribute(AttributeNames.Muted, value ? String.Empty : null); }
        }

        public Boolean IsPaused
        {
            get { return PlaybackState == MediaControllerPlaybackState.Waiting && ReadyState >= MediaReadyState.CurrentData; }
        }

        public Boolean IsEnded
        {
            get { return PlaybackState == MediaControllerPlaybackState.Ended; }
        }

        public DateTime StartDate
        {
            get { return DateTime.Today; }
        }

        public ITimeRanges BufferedTime
        {
            get { return _controller != null ? _controller.BufferedTime : null; }
        }

        public ITimeRanges SeekableTime
        {
            get { return _controller != null ? _controller.SeekableTime : null; }
        }

        public ITimeRanges PlayedTime
        {
            get { return _controller != null ? _controller.PlayedTime : null; }
        }

        public String MediaGroup
        {
            get { return GetAttribute(AttributeNames.MediaGroup); }
            set { SetAttribute(AttributeNames.MediaGroup, value); }
        }

        public Double Volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        public Boolean IsMuted
        {
            get { return _muted.HasValue ? _muted.Value : IsDefaultMuted; }
            set { _muted = value; }
        }

        public IMediaController Controller
        {
            get { return _controller; }
        }

        public Double DefaultPlaybackRate
        {
            get { return _controller != null ? _controller.DefaultPlaybackRate : 1.0; }
            set { if (_controller != null) _controller.DefaultPlaybackRate = value; }
        }

        public Double PlaybackRate
        {
            get { return _controller != null ? _controller.PlaybackRate : 1.0; }
            set { if (_controller != null) _controller.PlaybackRate = value; }
        }

        public MediaControllerPlaybackState PlaybackState
        {
            get { return _controller != null ? _controller.PlaybackState : MediaControllerPlaybackState.Waiting; }
        }

        public IMediaError Error
        {
            get;
            private set;
        }

        public IAudioTrackList AudioTracks
        {
            get { return _audios; }
        }

        public IVideoTrackList VideoTracks
        {
            get { return _videos; }
        }

        public ITextTrackList TextTracks
        {
            get { return _texts; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the media specified for this element.
        /// </summary>
        public void Load()
        {
            //TODO
        }

        /// <summary>
        /// Tries to play the media for this element.
        /// </summary>
        public void Play()
        {
            if (_controller != null)
                _controller.Play();
        }

        /// <summary>
        /// Pauses the playback of the media for this element.
        /// </summary>
        public void Pause()
        {
            if (_controller != null)
                _controller.Pause();
        }

        public String CanPlayType(String type)
        {
            //Does not play anything at the moment
            return String.Empty;
        }

        public ITextTrack AddTextTrack(String kind, String label = null, String language = null)
        {
            //TODO
            return null;
        }

        #endregion
    }
}
