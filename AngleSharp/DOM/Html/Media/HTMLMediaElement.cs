using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the abstract base for HTML media (audio / video) elements.
    /// </summary>
    public abstract class HTMLMediaElement : HTMLElement
    {
        #region Members

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
        [DOM("src")]
        public String Src
        {
            get { return GetAttribute("src"); }
            set { SetAttribute("src", value); }
        }

        /// <summary>
        /// Gets or sets the cross-origin attribute.
        /// </summary>
        [DOM("crossOrigin")]
        public String CrossOrigin
        {
            get { return GetAttribute("crossorigin"); }
            set { SetAttribute("crossorigin", value); }
        }

        /// <summary>
        /// Gets or sets the preload attribute.
        /// </summary>
        [DOM("preload")]
        public String Preload
        {
            get { return GetAttribute("preload"); }
            set { SetAttribute("preload", value); }
        }

        /// <summary>
        /// Gets the current network state.
        /// </summary>
        [DOM("networkState")]
        public MediaNetworkState NetworkState
        {
            get { return _network; }
        }

        /// <summary>
        /// Gets the current ready state.
        /// </summary>
        [DOM("readyState")]
        public MediaReadyState ReadyState
        {
            get { return _ready; }
        }

        /// <summary>
        /// Gets if seeking is currently active.
        /// </summary>
        [DOM("seeking")]
        public Boolean Seeking
        {
            get { return _seeking; }
        }

        /// <summary>
        /// Gets the current media source.
        /// </summary>
        [DOM("currentSrc")]
        public String CurrentSrc
        {
            get { return _source; }
        }

        /// <summary>
        /// Gets the time in seconds.
        /// </summary>
        [DOM("duration")]
        public Double Duration
        {
            get { return _duration; }
        }

        /// <summary>
        /// Gets or sets the current time in seconds.
        /// </summary>
        [DOM("currentTime")]
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
        [DOM("load")]
        public void Load()
        {
            //TODO
        }

        /// <summary>
        /// Tries to play the media for this element.
        /// </summary>
        [DOM("play")]
        public void Play()
        {
            //TODO
        }

        /// <summary>
        /// Pauses the playback of the media for this element.
        /// </summary>
        [DOM("pause")]
        public void Pause()
        {
            //TODO
        }

        #endregion

        #region Enumerations

        /// <summary>
        /// An enumeration of possible network states.
        /// </summary>
        public enum MediaNetworkState : ushort
        {
            /// <summary>
            /// The element has not yet been initialized.
            /// Everything is in initial state.
            /// </summary>
            Empty = 0,
            /// <summary>
            /// The element's resource selection alg. is active.
            /// No network usage at the moment, but nothing
            /// loaded.
            /// </summary>
            Idle = 1,
            /// <summary>
            /// The download is in progress.
            /// </summary>
            Loading = 2,
            /// <summary>
            /// The element's resource selection alg. is active,
            /// but has not yet found a resource to use.
            /// </summary>
            NoSource = 3
        }

        /// <summary>
        /// An enumeration of media ready states.
        /// </summary>
        public enum MediaReadyState : ushort
        {
            /// <summary>
            /// No information is available.
            /// </summary>
            Nothing = 0,
            /// <summary>
            /// Enough information obtained such that the duration of the
            /// resource is available.
            /// </summary>
            Metadata = 1,
            /// <summary>
            /// Data for immediate playback is available, but not enough
            /// to advance.
            /// </summary>
            CurrentData = 2,
            /// <summary>
            /// Enough data for the current and further positions are 
            /// available.
            /// </summary>
            FutureData = 3,
            /// <summary>
            /// All conditions are met and playback should immediately
            /// execute.
            /// </summary>
            EnoughData = 4
        }

        #endregion
    }
}
