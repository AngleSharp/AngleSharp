namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Media;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using AngleSharp.Network.RequestProcessors;
    using AngleSharp.Services.Media;
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
            _request = MediaRequestProcessor<TResource>.Create(this);
        }

        #endregion

        #region Properties

        public IDownload CurrentDownload
        {
            get { return _request?.Download; }
        }

        public String Source
        {
            get { return this.GetUrlAttribute(AttributeNames.Src); }
            set { this.SetOwnAttribute(AttributeNames.Src, value); }
        }

        public String CrossOrigin
        {
            get { return this.GetOwnAttribute(AttributeNames.CrossOrigin); }
            set { this.SetOwnAttribute(AttributeNames.CrossOrigin, value); }
        }

        public String Preload
        {
            get { return this.GetOwnAttribute(AttributeNames.Preload); }
            set { this.SetOwnAttribute(AttributeNames.Preload, value); }
        }

        public MediaNetworkState NetworkState
        {
            get { return _request?.NetworkState ?? MediaNetworkState.Empty; }
        }

        public TResource Media
        {
            get { return _request?.Resource; }
        }

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

        public String CurrentSource
        {
            get
            {
                //TODO Check for Source elements
                return Source; 
            }
        }

        public Double Duration
        {
            get { return Controller?.Duration ?? 0.0; }
        }

        public Double CurrentTime
        {
            get { return Controller?.CurrentTime ?? 0.0; }
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
            get { return this.GetBoolAttribute(AttributeNames.Autoplay); }
            set { this.SetBoolAttribute(AttributeNames.Autoplay, value); }
        }

        public Boolean IsLoop
        {
            get { return this.GetBoolAttribute(AttributeNames.Loop); }
            set { this.SetBoolAttribute(AttributeNames.Loop, value); }
        }

        public Boolean IsShowingControls
        {
            get { return this.GetBoolAttribute(AttributeNames.Controls); }
            set { this.SetBoolAttribute(AttributeNames.Controls, value); }
        }

        public Boolean IsDefaultMuted
        {
            get { return this.GetBoolAttribute(AttributeNames.Muted); }
            set { this.SetBoolAttribute(AttributeNames.Muted, value); }
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
            get { return Controller?.BufferedTime; }
        }

        public ITimeRanges SeekableTime
        {
            get { return Controller?.SeekableTime; }
        }

        public ITimeRanges PlayedTime
        {
            get { return Controller?.PlayedTime; }
        }

        public String MediaGroup
        {
            get { return this.GetOwnAttribute(AttributeNames.MediaGroup); }
            set { this.SetOwnAttribute(AttributeNames.MediaGroup, value); }
        }

        public Double Volume
        {
            get { return Controller?.Volume ?? 1.0; }
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
            get { return Controller?.IsMuted ?? false; }
            set
            {
                var controller = Controller;

                if (controller != null)
                {
                    controller.IsMuted = value;
                }
            }
        }

        public IMediaController Controller
        {
            get { return _request?.Resource?.Controller; }
        }

        public Double DefaultPlaybackRate
        {
            get { return Controller?.DefaultPlaybackRate ?? 1.0; }
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
            get { return Controller?.PlaybackRate ?? 1.0; }
            set
            {
                var controller = Controller;

                if (controller != null)
                {
                    controller.PlaybackRate = value;
                }
            }
        }

        public MediaControllerPlaybackState PlaybackState
        {
            get { return Controller?.PlaybackState ?? MediaControllerPlaybackState.Waiting; }
        }

        public IMediaError MediaError
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
            var service = Owner?.Options.GetResourceService<TResource>(type);
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

            var src = this.GetOwnAttribute(AttributeNames.Src);

            if (src != null)
            {
                UpdateSource(src);
            }
        }

        internal void UpdateSource(String value)
        {
            var url = new Url(value);
            this.Process(_request, url);
        }

        #endregion
    }
}
