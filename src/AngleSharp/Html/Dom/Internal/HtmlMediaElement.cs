namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using AngleSharp.Io.Processors;
    using AngleSharp.Media;
    using AngleSharp.Media.Dom;
    using System;

    /// <summary>
    /// Represents the abstract base for HTML media (audio / video) elements.
    /// </summary>
    abstract class HtmlMediaElement<TResource> : HtmlElement, IHtmlMediaElement
        where TResource : class, IMediaInfo
    {
        #region Fields

        private readonly MediaRequestProcessor<TResource> _request;
        private ITextTrackList _texts;

        #endregion

        #region ctor
        
        public HtmlMediaElement(Document owner, String name, String prefix)
            : base(owner, name, prefix)
        {
            _request = new MediaRequestProcessor<TResource>(owner.Context);
        }

        #endregion

        #region Properties

        public IDownload CurrentDownload => _request?.Download;

        public String Source
        {
            get => this.GetUrlAttribute(AttributeNames.Src);
            set => this.SetOwnAttribute(AttributeNames.Src, value);
        }

        public String CrossOrigin
        {
            get => this.GetOwnAttribute(AttributeNames.CrossOrigin);
            set => this.SetOwnAttribute(AttributeNames.CrossOrigin, value);
        }

        public String Preload
        {
            get => this.GetOwnAttribute(AttributeNames.Preload);
            set => this.SetOwnAttribute(AttributeNames.Preload, value);
        }

        public MediaNetworkState NetworkState => _request?.NetworkState ?? MediaNetworkState.Empty;

        public TResource Media => _request?.Resource;

        public MediaReadyState ReadyState
        {
            get 
            { 
                var controller = Controller; 
                return controller == null ? MediaReadyState.Nothing : controller.ReadyState; 
            }
        }

        public Boolean IsSeeking
        {
            get;
            protected set;
        }

        public String CurrentSource =>
                //TODO Check for Source elements
                Source;

        public Double Duration => Controller?.Duration ?? 0.0;

        public Double CurrentTime
        {
            get => Controller?.CurrentTime ?? 0.0;
            set
            {
                var controller = Controller;

                if (controller != null)
                {
                    controller.CurrentTime = value;
                }

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
            get => this.GetBoolAttribute(AttributeNames.Autoplay);
            set => this.SetBoolAttribute(AttributeNames.Autoplay, value);
        }

        public Boolean IsLoop
        {
            get => this.GetBoolAttribute(AttributeNames.Loop);
            set => this.SetBoolAttribute(AttributeNames.Loop, value);
        }

        public Boolean IsShowingControls
        {
            get => this.GetBoolAttribute(AttributeNames.Controls);
            set => this.SetBoolAttribute(AttributeNames.Controls, value);
        }

        public Boolean IsDefaultMuted
        {
            get => this.GetBoolAttribute(AttributeNames.Muted);
            set => this.SetBoolAttribute(AttributeNames.Muted, value);
        }

        public Boolean IsPaused => PlaybackState == MediaControllerPlaybackState.Waiting && ReadyState >= MediaReadyState.CurrentData;

        public Boolean IsEnded => PlaybackState == MediaControllerPlaybackState.Ended;

        public DateTime StartDate => DateTime.Today;

        public ITimeRanges BufferedTime => Controller?.BufferedTime;

        public ITimeRanges SeekableTime => Controller?.SeekableTime;

        public ITimeRanges PlayedTime => Controller?.PlayedTime;

        public String MediaGroup
        {
            get => this.GetOwnAttribute(AttributeNames.MediaGroup);
            set => this.SetOwnAttribute(AttributeNames.MediaGroup, value);
        }

        public Double Volume
        {
            get => Controller?.Volume ?? 1.0;
            set
            {
                var controller = Controller;

                if (controller != null)
                {
                    controller.Volume = value;
                }
            }
        }

        public Boolean IsMuted
        {
            get => Controller?.IsMuted ?? false;
            set
            {
                var controller = Controller;

                if (controller != null)
                {
                    controller.IsMuted = value;
                }
            }
        }

        public IMediaController Controller => _request?.Resource?.Controller;

        public Double DefaultPlaybackRate
        {
            get => Controller?.DefaultPlaybackRate ?? 1.0;
            set
            {
                var controller = Controller;

                if (controller != null)
                {
                    controller.DefaultPlaybackRate = value;
                }
            }
        }

        public Double PlaybackRate
        {
            get => Controller?.PlaybackRate ?? 1.0;
            set
            {
                var controller = Controller;

                if (controller != null)
                {
                    controller.PlaybackRate = value;
                }
            }
        }

        public MediaControllerPlaybackState PlaybackState => Controller?.PlaybackState ?? MediaControllerPlaybackState.Waiting;

        public IMediaError MediaError
        {
            get;
            private set;
        }

        public virtual IAudioTrackList AudioTracks => null;

        public virtual IVideoTrackList VideoTracks => null;

        public ITextTrackList TextTracks
        {
            get => _texts;
            protected set => _texts = value;
        }

        #endregion

        #region Methods

        public void Load()
        {
            var source = CurrentSource;
            UpdateSource(source);
        }

        public void Play()
        {
            Controller?.Play();
        }

        public void Pause()
        {
            Controller?.Pause();
        }

        public String CanPlayType(String type)
        {
            var service = Context?.GetResourceService<TResource>(type);
            //Other option would be probably.
            return service != null ? "maybe" : String.Empty;
        }

        public ITextTrack AddTextTrack(String kind, String label = null, String language = null)
        {
            //TODO
            return null;
        }

        #endregion

        #region Internal Methods

        internal override void SetupElement()
        {
            base.SetupElement();
            UpdateSource(this.GetOwnAttribute(AttributeNames.Src));
        }

        internal void UpdateSource(String value)
        {
            if (value != null)
            {
                var url = new Url(value);
                this.Process(_request, url);
            }
        }

        #endregion
    }
}
