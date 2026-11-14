namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using AngleSharp.Io.Processors;
    using AngleSharp.Media;
    using AngleSharp.Media.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the abstract base for HTML media (audio / video) elements.
    /// The element keeps what a script sets (paused, volume, muted, the rates and a seek)
    /// until its resource arrives and hands it to the resource's controller then. It fires
    /// the events of the state it changes itself (play, pause, volumechange, ratechange,
    /// seeking and seeked) and those of the controller (e.g. playing, timeupdate, ended).
    /// </summary>
    abstract class HtmlMediaElement<TResource> : HtmlElement, IHtmlMediaElement, IPlayableMediaElement
        where TResource : class, IMediaInfo
    {
        #region Fields

        private MediaRequestProcessor<TResource> _request;
        private ITextTrackList? _texts;
        private IMediaController? _controller;
        private Action? _detach;
        private List<TaskCompletionSource<Boolean>>? _pendingPlays;
        private Boolean _paused;
        private Boolean _playing;
        private Boolean? _muted;
        private Double _volume;
        private Double _playbackRate;
        private Double _defaultPlaybackRate;
        private Double? _pendingTime;

        #endregion

        #region ctor

        public HtmlMediaElement(Document owner, String name, String? prefix)
            : base(owner, name, prefix)
        {
            _request = CreateProcessor(owner.Context);
            _paused = true;
            _volume = 1.0;
            _playbackRate = 1.0;
            _defaultPlaybackRate = 1.0;
        }

        #endregion

        #region Properties

        public IDownload? CurrentDownload => _request?.Download;

        public String? Source
        {
            get => this.GetUrlAttribute(AttributeNames.Src);
            set => this.SetOwnAttribute(AttributeNames.Src, value);
        }

        public String? CrossOrigin
        {
            get => this.GetOwnAttribute(AttributeNames.CrossOrigin);
            set => this.SetOwnAttribute(AttributeNames.CrossOrigin, value);
        }

        public String? Preload
        {
            get => this.GetOwnAttribute(AttributeNames.Preload);
            set => this.SetOwnAttribute(AttributeNames.Preload, value);
        }

        public MediaNetworkState NetworkState => _request?.NetworkState ?? MediaNetworkState.Empty;

        public TResource? Media => _request?.Resource;

        public MediaReadyState ReadyState => _controller?.ReadyState ?? MediaReadyState.Nothing;

        public Boolean IsSeeking
        {
            get;
            protected set;
        }

        public String? CurrentSource
        {
            get
            {
                var source = Source;

                if (!String.IsNullOrEmpty(source))
                {
                    return source;
                }

                for (var i = 0; i < ChildElementCount; i++)
                {
                    if (Children[i] is IHtmlSourceElement childSource && !String.IsNullOrEmpty(childSource.Source) && IsPlayable(childSource.Type))
                    {
                        return childSource.Source;
                    }
                }

                return source;
            }
        }

        public Double Duration => _controller?.Duration ?? Double.NaN;

        public Double CurrentTime
        {
            get => _controller?.CurrentTime ?? _pendingTime ?? 0.0;
            set
            {
                var time = Math.Max(0.0, value);
                var controller = _controller;

                if (controller != null)
                {
                    controller.CurrentTime = time;
                }
                else
                {
                    _pendingTime = time;
                }

                QueueEvent(EventNames.Seeking);
                QueueEvent(EventNames.TimeUpdate);
                QueueEvent(EventNames.Seeked);
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

        public Boolean IsPaused => _paused;

        public Boolean IsEnded => !IsLoop && PlaybackState == MediaControllerPlaybackState.Ended;

        public DateTime StartDate => DateTime.Today;

        public ITimeRanges? BufferedTime => _controller?.BufferedTime;

        public ITimeRanges? SeekableTime => _controller?.SeekableTime;

        public ITimeRanges? PlayedTime => _controller?.PlayedTime;

        public String? MediaGroup
        {
            get => this.GetOwnAttribute(AttributeNames.MediaGroup);
            set => this.SetOwnAttribute(AttributeNames.MediaGroup, value);
        }

        public Double Volume
        {
            get => _volume;
            set
            {
                if (value < 0.0 || value > 1.0)
                {
                    throw new DomException(DomError.IndexSizeError);
                }

                if (value != _volume)
                {
                    _volume = value;

                    if (_controller != null)
                    {
                        _controller.Volume = value;
                    }

                    QueueEvent(EventNames.VolumeChange);
                }
            }
        }

        public Boolean IsMuted
        {
            get => _muted ?? IsDefaultMuted;
            set
            {
                if (value != IsMuted)
                {
                    _muted = value;

                    if (_controller != null)
                    {
                        _controller.IsMuted = value;
                    }

                    QueueEvent(EventNames.VolumeChange);
                }
                else
                {
                    _muted = value;
                }
            }
        }

        public IMediaController? Controller => _controller;

        public Double DefaultPlaybackRate
        {
            get => _defaultPlaybackRate;
            set
            {
                if (value != _defaultPlaybackRate)
                {
                    _defaultPlaybackRate = value;

                    if (_controller != null)
                    {
                        _controller.DefaultPlaybackRate = value;
                    }

                    QueueEvent(EventNames.RateChange);
                }
            }
        }

        public Double PlaybackRate
        {
            get => _playbackRate;
            set
            {
                if (value != _playbackRate)
                {
                    _playbackRate = value;

                    if (_controller != null)
                    {
                        _controller.PlaybackRate = value;
                    }

                    QueueEvent(EventNames.RateChange);
                }
            }
        }

        public MediaControllerPlaybackState PlaybackState => _controller?.PlaybackState ?? MediaControllerPlaybackState.Waiting;

        public IMediaError? MediaError
        {
            get;
            private set;
        }

        public virtual IAudioTrackList? AudioTracks => null;

        public virtual IVideoTrackList? VideoTracks => null;

        public ITextTrackList? TextTracks
        {
            get => _texts;
            protected set => _texts = value;
        }

        #endregion

        #region Methods

        public void Load()
        {
            var source = SelectedSource;
            var media = Media;
            StopPlaying();

            if (_controller != null && media != null && source != null && this.HyperReference(source) is { } url && url.Equals(media.Source))
            {
                //  the resource stays, it starts over
                _controller.Pause();
                _controller.CurrentTime = 0.0;
                PlayIfAutoplay();
            }
            else
            {
                UpdateSource(source);
            }
        }

        public void Play() => PlayAsync();

        /// <summary>
        /// Plays the media, see <see cref="HtmlMediaElementExtensions.PlayAsync(IHtmlMediaElement)"/>.
        /// </summary>
        public Task PlayAsync()
        {
            if (MediaError is { Code: MediaErrorCode.SourceNotSupported })
            {
                return FromException(new DomException(DomError.NotSupported));
            }

            var controller = _controller;

            if (controller != null && _paused)
            {
                try
                {
                    StartController(controller);
                }
                catch (DomException ex)
                {
                    //  e.g. a NotAllowedError of an autoplay policy
                    return FromException(ex);
                }
            }

            if (_paused)
            {
                _paused = false;
                _playing = false;
                QueueEvent(EventNames.Play);
            }
            else if (_playing)
            {
                return Task.CompletedTask;
            }

            var pending = new TaskCompletionSource<Boolean>();
            (_pendingPlays ??= new List<TaskCompletionSource<Boolean>>()).Add(pending);
            return pending.Task;
        }

        public void Pause()
        {
            if (!_paused)
            {
                _paused = true;
                _playing = false;
                QueueEvent(EventNames.TimeUpdate);
                QueueEvent(EventNames.Pause);
                RejectPendingPlays(new DomException(DomError.Abort));
            }

            _controller?.Pause();
        }

        public String CanPlayType(String type)
        {
            var service = Context?.GetResourceService<TResource>(type);
            //Other option would be probably.
            return service != null ? "maybe" : String.Empty;
        }

        public ITextTrack AddTextTrack(String kind, String? label = null, String? language = null)
        {
            //TODO
            return null!;
        }

        #endregion

        #region Internal Methods

        internal override void SetupElement()
        {
            base.SetupElement();
            UpdateSource(SelectedSource);
        }

        /// <summary>
        /// Like an image, a media element loads outside of a document, too, but not in the one
        /// of the fragment parser: adopting it resolves its source against the new document.
        /// </summary>
        protected override void NodeIsAdopted(Document oldDocument)
        {
            base.NodeIsAdopted(oldDocument);

            // it loads with the loader of its new document
            if (!ReferenceEquals(oldDocument.Context, Owner.Context))
            {
                Detach();
                _request = CreateProcessor(Owner.Context);
            }

            UpdateSource(SelectedSource);
        }

        /// <summary>
        /// A source element inserted by a script into an element without a source of its own
        /// loads (the parser sets the element up once its children are there).
        /// </summary>
        protected override void NodeIsInserted(Node newNode)
        {
            base.NodeIsInserted(newNode);

            if (newNode is IHtmlSourceElement && !this.HasOwnAttribute(AttributeNames.Src) && _controller is null && _request.Download is null)
            {
                UpdateSource(SelectedSource);
            }
        }

        /// <summary>
        /// A media element removed from its document pauses, unless it got back by then.
        /// </summary>
        protected override void OnParentChanged()
        {
            base.OnParentChanged();

            if (!_paused && Parent is null)
            {
                Owner?.QueueTask(() =>
                {
                    if (!_paused && !CustomElementRegistry.IsConnected(this))
                    {
                        Pause();
                    }
                });
            }
        }

        internal void UpdateSource(String? value)
        {
            if (value != null && Owner is not { IsFragmentParsing: true } && this.HyperReference(value) is { } url)
            {
                if (_controller != null && Media is { } media && !url.Equals(media.Source))
                {
                    //  another resource: the current one stops
                    StopPlaying();
                    Detach();
                }

                this.Process(_request, url);
            }
        }

        #endregion

        #region Helpers

        private MediaRequestProcessor<TResource> CreateProcessor(IBrowsingContext? context) =>
            new MediaRequestProcessor<TResource>(context!, OnMediaCreated, OnMediaFailed);

        /// <summary>
        /// The source to load: the element's own (even if empty), else the first one of its
        /// source elements of a type that plays; null without any.
        /// </summary>
        private String? SelectedSource
        {
            get
            {
                var source = this.GetOwnAttribute(AttributeNames.Src);

                if (source is null)
                {
                    for (var i = 0; i < ChildElementCount; i++)
                    {
                        if (Children[i] is IHtmlSourceElement childSource && !String.IsNullOrEmpty(childSource.Source) && IsPlayable(childSource.Type))
                        {
                            return childSource.Source;
                        }
                    }
                }

                return source;
            }
        }

        private Boolean IsPlayable(String? type)
        {
            if (String.IsNullOrEmpty(type) || Context is not { } context)
            {
                return true;
            }

            //  without any service nothing plays, so the first source is as good as any other
            return !context.GetServices<IResourceService<TResource>>().Any() || CanPlayType(type!).Length > 0;
        }

        private void OnMediaCreated(TResource media) => Owner?.QueueTask(() => Attach(media));

        /// <summary>
        /// The resource failed before it played: it can't be played (or decoded), or the request
        /// failed, which counts as a source that isn't supported, too (HTML 4.8.11.5), and gets
        /// its error event here (the processor fires the one of a response it couldn't use).
        /// </summary>
        private void OnMediaFailed(MediaErrorCode code) => Owner?.QueueTask(() =>
        {
            MediaError = new MediaError(code == MediaErrorCode.Network ? MediaErrorCode.SourceNotSupported : code);

            RejectPendingPlays(new DomException(DomError.NotSupported));

            if (code == MediaErrorCode.Network)
            {
                FireEvent(EventNames.Error);
            }
        });

        private void Attach(TResource media)
        {
            if (!ReferenceEquals(media, Media) || media.Controller is not { } controller || ReferenceEquals(controller, _controller))
            {
                return;
            }

            Detach();
            MediaError = null;
            _controller = controller;
            Subscribe(controller);
            controller.Volume = _volume;
            controller.IsMuted = IsMuted;
            controller.DefaultPlaybackRate = _defaultPlaybackRate;
            controller.PlaybackRate = _playbackRate;

            if (_pendingTime is { } time)
            {
                _pendingTime = null;
                controller.CurrentTime = time;
            }

            //  what the controller has by now; it raises the events of what it gets later
            var readyState = controller.ReadyState;

            if (readyState >= MediaReadyState.Metadata)
            {
                FireEvent(EventNames.DurationChange);
                FireEvent(EventNames.LoadedMetaData);
            }

            if (readyState >= MediaReadyState.CurrentData)
            {
                FireEvent(EventNames.LoadedData);
            }

            if (readyState >= MediaReadyState.FutureData)
            {
                FireEvent(EventNames.CanPlay);
            }

            if (readyState >= MediaReadyState.EnoughData)
            {
                FireEvent(EventNames.CanPlayThrough);
            }

            if (!_paused)
            {
                try
                {
                    StartController(controller);
                }
                catch (DomException ex)
                {
                    _paused = true;
                    FireEvent(EventNames.Pause);
                    RejectPendingPlays(ex);
                }
            }
            else
            {
                PlayIfAutoplay();
            }
        }

        private void PlayIfAutoplay()
        {
            if (IsAutoplay)
            {
                //  a rejection (e.g. by an autoplay policy) just leaves it paused
                PlayAsync().ContinueWith(task => task.Exception, TaskContinuationOptions.OnlyOnFaulted);
            }
        }

        private void Subscribe(IMediaController controller)
        {
            DomEventHandler Relay(String name) => (sender, ev) => Owner?.QueueTask(() =>
            {
                if (ReferenceEquals(_controller, controller))
                {
                    OnControllerEvent(name);
                }
            });

            var emptied = Relay(EventNames.Emptied);
            var loadedMetadata = Relay(EventNames.LoadedMetaData);
            var loadedData = Relay(EventNames.LoadedData);
            var canPlay = Relay(EventNames.CanPlay);
            var canPlayThrough = Relay(EventNames.CanPlayThrough);
            var ended = Relay(EventNames.Ended);
            var waiting = Relay(EventNames.Waiting);
            var durationChanged = Relay(EventNames.DurationChange);
            var timeUpdated = Relay(EventNames.TimeUpdate);
            var paused = Relay(EventNames.Pause);
            var playing = Relay(EventNames.Playing);

            controller.Emptied += emptied;
            controller.LoadedMetadata += loadedMetadata;
            controller.LoadedData += loadedData;
            controller.CanPlay += canPlay;
            controller.CanPlayThrough += canPlayThrough;
            controller.Ended += ended;
            controller.Waiting += waiting;
            controller.DurationChanged += durationChanged;
            controller.TimeUpdated += timeUpdated;
            controller.Paused += paused;
            controller.Playing += playing;

            _detach = () =>
            {
                controller.Emptied -= emptied;
                controller.LoadedMetadata -= loadedMetadata;
                controller.LoadedData -= loadedData;
                controller.CanPlay -= canPlay;
                controller.CanPlayThrough -= canPlayThrough;
                controller.Ended -= ended;
                controller.Waiting -= waiting;
                controller.DurationChanged -= durationChanged;
                controller.TimeUpdated -= timeUpdated;
                controller.Paused -= paused;
                controller.Playing -= playing;
            };
        }

        private void Detach()
        {
            var controller = _controller;
            _detach?.Invoke();
            _detach = null;
            _controller = null;
            _playing = false;
            controller?.Pause();
        }

        private void OnControllerEvent(String name)
        {
            if (name == EventNames.Ended)
            {
                if (IsLoop && _controller is { } controller)
                {
                    controller.CurrentTime = 0.0;

                    if (!_paused)
                    {
                        controller.Play();
                    }

                    return;
                }

                if (!_paused)
                {
                    _paused = true;
                    _playing = false;
                    FireEvent(EventNames.Pause);
                    RejectPendingPlays(new DomException(DomError.Abort));
                }
            }
            else if (name == EventNames.Pause)
            {
                //  the controller paused by itself (e.g. its host stopped it)
                if (!_paused)
                {
                    _paused = true;
                    _playing = false;
                    FireEvent(EventNames.Pause);
                    RejectPendingPlays(new DomException(DomError.Abort));
                }

                return;
            }
            else if (name == EventNames.Playing)
            {
                if (_paused)
                {
                    return;
                }

                _playing = true;
                ResolvePendingPlays();
            }

            FireEvent(name);
        }

        private void StartController(IMediaController controller)
        {
            if (controller.PlaybackState == MediaControllerPlaybackState.Ended)
            {
                controller.CurrentTime = 0.0;
            }

            controller.Play();
        }

        private void StopPlaying()
        {
            if (!_paused)
            {
                _paused = true;
                _playing = false;
                QueueEvent(EventNames.Pause);
            }

            RejectPendingPlays(new DomException(DomError.Abort));
        }

        private void ResolvePendingPlays()
        {
            var pending = _pendingPlays;
            _pendingPlays = null;
            pending?.ForEach(play => play.TrySetResult(true));
        }

        private void RejectPendingPlays(Exception error)
        {
            var pending = _pendingPlays;
            _pendingPlays = null;
            pending?.ForEach(play => play.TrySetException(error));
        }

        private void QueueEvent(String name) => Owner?.QueueTask(() => FireEvent(name));

        private void FireEvent(String name) => this.FireSimpleEvent(name);

        private static Task FromException(Exception error)
        {
            var failed = new TaskCompletionSource<Boolean>();
            failed.SetException(error);
            return failed.Task;
        }

        #endregion
    }
}
