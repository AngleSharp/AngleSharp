namespace AngleSharp.Dom.Html
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp.Dom.Media;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Services;
    using AngleSharp.Services.Media;

    /// <summary>
    /// Represents the abstract base for HTML media (audio / video) elements.
    /// </summary>
    abstract class HTMLMediaElement<TResource> : HtmlElement, IHtmlMediaElement, IDisposable
        where TResource : IMediaInfo
    {
        #region Fields

        readonly BoundLocation _src;
        protected MediaNetworkState _network;
        protected Task<TResource> _resourceTask;

        CancellationTokenSource _cts;
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

        public HTMLMediaElement(Document owner, String name, String prefix)
            : base(owner, name, prefix)
        {
            _src = new BoundLocation(this, AttributeNames.Src);
            _network = MediaNetworkState.Empty;
            RegisterAttributeObserver(AttributeNames.Src, value => Load());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the media source.
        /// </summary>
        public String Source
        {
            get { return _src.Href; }
            set { _src.Href = value; }
        }

        /// <summary>
        /// Gets or sets the cross-origin attribute.
        /// </summary>
        public String CrossOrigin
        {
            get { return GetOwnAttribute(AttributeNames.CrossOrigin); }
            set { SetOwnAttribute(AttributeNames.CrossOrigin, value); }
        }

        /// <summary>
        /// Gets or sets the preload attribute.
        /// </summary>
        public String Preload
        {
            get { return GetOwnAttribute(AttributeNames.Preload); }
            set { SetOwnAttribute(AttributeNames.Preload, value); }
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
            get 
            { 
                var controller = Controller; 
                return controller == null ? MediaReadyState.Nothing : controller.ReadyState; 
            }
        }

        /// <summary>
        /// Gets if seeking is currently active.
        /// </summary>
        public Boolean IsSeeking
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the current media source.
        /// </summary>
        public String CurrentSource
        {
            get
            {
                //TODO Check for Source elements
                return Source; 
            }
        }

        /// <summary>
        /// Gets the time in seconds.
        /// </summary>
        public Double Duration
        {
            get 
            {
                var controller = Controller;
                return controller != null ? controller.Duration : 0.0; 
            }
        }

        /// <summary>
        /// Gets or sets the current time in seconds.
        /// </summary>
        public Double CurrentTime
        {
            get 
            {
                var controller = Controller;
                return controller != null ? controller.CurrentTime : 0.0; }
            set
            {
                var controller = Controller;

                if (controller != null)
                    controller.CurrentTime = value;

                //if (value < 0)
                //    _currentTime = 0;
                //else if (value > Duration)
                //    _currentTime = Duration;
                //else
                //    _currentTime = value;

                //var ev = new Event();
                //ev.Init(EventNames.DurationChange, true, true);
                //Dispatch(ev);
            }
        }

        public Boolean IsAutoplay
        {
            get { return GetOwnAttribute(AttributeNames.Autoplay) != null; }
            set { SetOwnAttribute(AttributeNames.Autoplay, value ? String.Empty : null); }
        }

        public Boolean IsLoop
        {
            get { return GetOwnAttribute(AttributeNames.Loop) != null; }
            set { SetOwnAttribute(AttributeNames.Loop, value ? String.Empty : null); }
        }

        public Boolean IsShowingControls
        {
            get { return GetOwnAttribute(AttributeNames.Controls) != null; }
            set { SetOwnAttribute(AttributeNames.Controls, value ? String.Empty : null); }
        }

        public Boolean IsDefaultMuted
        {
            get { return GetOwnAttribute(AttributeNames.Muted) != null; }
            set { SetOwnAttribute(AttributeNames.Muted, value ? String.Empty : null); }
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
            get 
            {
                var controller = Controller;
                return controller != null ? controller.BufferedTime : null; 
            }
        }

        public ITimeRanges SeekableTime
        {
            get 
            {
                var controller = Controller;
                return controller != null ? controller.SeekableTime : null; 
            }
        }

        public ITimeRanges PlayedTime
        {
            get 
            {
                var controller = Controller;
                return controller != null ? controller.PlayedTime : null;
            }
        }

        public String MediaGroup
        {
            get { return GetOwnAttribute(AttributeNames.MediaGroup); }
            set { SetOwnAttribute(AttributeNames.MediaGroup, value); }
        }

        public Double Volume
        {
            get
            {
                var controller = Controller; 
                return controller != null ? controller.Volume : 1.0;
            }
            set
            {
                var controller = Controller;
                
                if (controller != null) 
                    controller.Volume = value;
            }
        }

        public Boolean IsMuted
        {
            get
            {
                var controller = Controller; 
                return controller != null ? controller.IsMuted : false;
            }
            set
            {
                var controller = Controller; 
                
                if (controller != null) 
                    controller.IsMuted = value;
            }
        }

        public IMediaController Controller
        {
            get { return _resourceTask != null && _resourceTask.IsCompleted && _resourceTask.Result != null ? _resourceTask.Result.Controller : null; }
        }

        public Double DefaultPlaybackRate
        {
            get
            {
                var controller = Controller; 
                return controller != null ? controller.DefaultPlaybackRate : 1.0;
            }
            set
            {
                var controller = Controller; 
                
                if  (controller != null) 
                    controller.DefaultPlaybackRate = value;
            }
        }

        public Double PlaybackRate
        {
            get
            {
                var controller = Controller;
                return controller != null ? controller.PlaybackRate : 1.0;
            }
            set
            {
                var controller = Controller; 
                
                if (controller != null) 
                    controller.PlaybackRate = value;
            }
        }

        public MediaControllerPlaybackState PlaybackState
        {
            get
            {
                var controller = Controller; 
                return controller != null ? controller.PlaybackState : MediaControllerPlaybackState.Waiting;
            }
        }

        public IMediaError Error
        {
            get;
            private set;
        }

        public virtual IAudioTrackList AudioTracks
        {
            get { return null; }
        }

        public virtual IVideoTrackList VideoTracks
        {
            get { return null; }
        }

        public ITextTrackList TextTracks
        {
            get { return _texts; }
            protected set { _texts = value; }
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            if (_cts != null)
                _cts.Cancel();

            _cts = null;
            _resourceTask = null;
        }

        /// <summary>
        /// Loads the media specified for this element.
        /// </summary>
        public void Load()
        {
            //TODO More complex check if something is already loading (what is loading, cancel?, ...)
            //see: https://html.spec.whatwg.org/multipage/embedded-content.html#dom-media-load
            if (_resourceTask != null)
                return;
            else if (_cts != null)
                _cts.Cancel();
            
            var src = CurrentSource;

            if (src != null)
            {
                _network = MediaNetworkState.Idle;
                var url = new Url(src);
                _cts = new CancellationTokenSource();
                _network = MediaNetworkState.Loading;
                _resourceTask = LoadAsync(url, _cts.Token);
            }
        }

        async Task<TResource> LoadAsync(Url url, CancellationToken cancel)
        {
            var request = this.CreateRequestFor(url);
            var media = await Owner.LoadResource<TResource>(request, cancel).ConfigureAwait(false);

            if (media == null)
                _network = MediaNetworkState.NoSource;

            this.FireSimpleEvent(EventNames.Load);
            return media;
        }

        /// <summary>
        /// Tries to play the media for this element.
        /// </summary>
        public void Play()
        {
            var controller = Controller;

            if (controller != null)
                controller.Play();
        }

        /// <summary>
        /// Pauses the playback of the media for this element.
        /// </summary>
        public void Pause()
        {
            var controller = Controller;

            if (controller != null)
                controller.Pause();
        }

        public String CanPlayType(String type)
        {
            var services = Owner.Options.GetServices<IResourceService<TResource>>();

            foreach (var service in services)
            {
                if (service.SupportsType(type))
                    return "maybe";//Other option would be probably.
            }

            //Cannot be played.
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
