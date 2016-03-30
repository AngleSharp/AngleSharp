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
        where TResource : IMediaInfo
    {
        #region Fields

        readonly MediaRequestProcessor<TResource> _request;
        ITextTrackList _texts;

        #endregion

        #region ctor

        static HtmlMediaElement()
        {
            RegisterCallback<HtmlMediaElement<TResource>>(AttributeNames.Src, (element, value) => element.UpdateSource(value));
        }

        public HtmlMediaElement(Document owner, String name, String prefix)
            : base(owner, name, prefix)
        {
            _request = MediaRequestProcessor<TResource>.Create(this);
        }

        #endregion

        #region Properties

        public IDownload CurrentDownload
        {
            get { return _request != null ? _request.Download : null; }
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
            get { return _request != null ? _request.NetworkState : MediaNetworkState.Empty; }
        }

        public TResource Media
        {
            get { return _request != null ? _request.Resource : default(TResource); }
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
            get 
            {
                var controller = Controller;
                return controller != null ? controller.Duration : 0.0; 
            }
        }

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
            get { return this.HasOwnAttribute(AttributeNames.Autoplay); }
            set { this.SetOwnAttribute(AttributeNames.Autoplay, value ? String.Empty : null); }
        }

        public Boolean IsLoop
        {
            get { return this.HasOwnAttribute(AttributeNames.Loop); }
            set { this.SetOwnAttribute(AttributeNames.Loop, value ? String.Empty : null); }
        }

        public Boolean IsShowingControls
        {
            get { return this.HasOwnAttribute(AttributeNames.Controls); }
            set { this.SetOwnAttribute(AttributeNames.Controls, value ? String.Empty : null); }
        }

        public Boolean IsDefaultMuted
        {
            get { return this.HasOwnAttribute(AttributeNames.Muted); }
            set { this.SetOwnAttribute(AttributeNames.Muted, value ? String.Empty : null); }
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
            get { return this.GetOwnAttribute(AttributeNames.MediaGroup); }
            set { this.SetOwnAttribute(AttributeNames.MediaGroup, value); }
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
                {
                    controller.Volume = value;
                }
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
                {
                    controller.IsMuted = value;
                }
            }
        }

        public IMediaController Controller
        {
            get { return _request != null && _request.Resource != null ? _request.Resource.Controller : null; }
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

                if (controller != null)
                {
                    controller.DefaultPlaybackRate = value;
                }
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
                {
                    controller.PlaybackRate = value;
                }
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
            var controller = Controller;

            if (controller != null)
            {
                controller.Play();
            }
        }

        public void Pause()
        {
            var controller = Controller;

            if (controller != null)
            {
                controller.Pause();
            }
        }

        public String CanPlayType(String type)
        {
            var service = Owner.Options.GetResourceService<TResource>(type);
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

        #endregion

        #region Helpers

        void UpdateSource(String value)
        {
            var url = new Url(value);
            this.Process(_request, url);
        }

        #endregion
    }
}
