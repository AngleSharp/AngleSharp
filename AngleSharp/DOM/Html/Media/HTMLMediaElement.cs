namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Media;
    using System;

    /// <summary>
    /// Represents the abstract base for HTML media (audio / video) elements.
    /// </summary>
    public abstract class HTMLMediaElement : HTMLElement
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
        /// The state of the media.
        /// </summary>
        protected MediaReadyState _ready;
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

        #endregion

        #region ctor

        internal HTMLMediaElement()
        {
        }

        #endregion

        #region Properties

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
        /// Gets or sets the cross-origin attribute.
        /// </summary>
        [DomName("crossOrigin")]
        public String CrossOrigin
        {
            get { return GetAttribute("crossorigin"); }
            set { SetAttribute("crossorigin", value); }
        }

        /// <summary>
        /// Gets or sets the preload attribute.
        /// </summary>
        [DomName("preload")]
        public String Preload
        {
            get { return GetAttribute("preload"); }
            set { SetAttribute("preload", value); }
        }

        /// <summary>
        /// Gets the current network state.
        /// </summary>
        [DomName("networkState")]
        public MediaNetworkState NetworkState
        {
            get { return _network; }
        }

        /// <summary>
        /// Gets the current ready state.
        /// </summary>
        [DomName("readyState")]
        public MediaReadyState ReadyState
        {
            get { return _ready; }
        }

        /// <summary>
        /// Gets if seeking is currently active.
        /// </summary>
        [DomName("seeking")]
        public Boolean Seeking
        {
            get { return _seeking; }
        }

        /// <summary>
        /// Gets the current media source.
        /// </summary>
        [DomName("currentSrc")]
        public String CurrentSrc
        {
            get { return _source; }
        }

        /// <summary>
        /// Gets the time in seconds.
        /// </summary>
        [DomName("duration")]
        public Double Duration
        {
            get { return _duration; }
        }

        /// <summary>
        /// Gets or sets the current time in seconds.
        /// </summary>
        [DomName("currentTime")]
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

                //TODO Current Time changed!
            }
        }

        //TODO
        //http://www.w3.org/TR/html5/embedded-content-0.html#htmlmediaelement

        #endregion

        #region Methods

        /// <summary>
        /// Loads the media specified for this element.
        /// </summary>
        [DomName("load")]
        public void Load()
        {
            //TODO
        }

        /// <summary>
        /// Tries to play the media for this element.
        /// </summary>
        [DomName("play")]
        public void Play()
        {
            //TODO
        }

        /// <summary>
        /// Pauses the playback of the media for this element.
        /// </summary>
        [DomName("pause")]
        public void Pause()
        {
            //TODO
        }

        #endregion
    }
}
