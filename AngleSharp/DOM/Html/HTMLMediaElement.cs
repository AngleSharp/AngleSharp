using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the abstract base for HTML media (audio / video) elements.
    /// </summary>
    public abstract class HTMLMediaElement : HTMLElement
    {
        #region Members

        protected string source;
        protected MediaNetworkState network;
        protected MediaReadyState ready;
        protected bool seeking;
        protected double duration;
        protected double currentTime;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the media source.
        /// </summary>
        public string Src
        {
            get { return GetAttribute("src"); }
            set { SetAttribute("src", value); }
        }

        /// <summary>
        /// Gets or sets the cross-origin attribute.
        /// </summary>
        public string CrossOrigin
        {
            get { return GetAttribute("crossorigin"); }
            set { SetAttribute("crossorigin", value); }
        }

        /// <summary>
        /// Gets or sets the preload attribute.
        /// </summary>
        public string Preload
        {
            get { return GetAttribute("preload"); }
            set { SetAttribute("preload", value); }
        }

        /// <summary>
        /// Gets the current network state.
        /// </summary>
        public MediaNetworkState NetworkState
        {
            get { return network; }
        }

        /// <summary>
        /// Gets the current ready state.
        /// </summary>
        public MediaReadyState ReadyState
        {
            get { return ready; }
        }

        /// <summary>
        /// Gets if seeking is currently active.
        /// </summary>
        public bool Seeking
        {
            get { return seeking; }
        }

        /// <summary>
        /// Gets the current media source.
        /// </summary>
        public string CurrentSrc
        {
            get { return source; }
        }

        /// <summary>
        /// Gets the time in seconds.
        /// </summary>
        public double Duration
        {
            get { return duration; }
        }

        /// <summary>
        /// Gets or sets the current time in seconds.
        /// </summary>
        public double CurrentTime
        {
            get { return currentTime; }
            set
            {
                if (value < 0)
                    currentTime = 0;
                else if (value > Duration)
                    currentTime = Duration;
                else
                    currentTime = value;

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
        public void Load()
        {
            //TODO
        }

        /// <summary>
        /// Tries to play the media for this element.
        /// </summary>
        public void Play()
        {
            //TODO
        }

        /// <summary>
        /// Pauses the playback of the media for this element.
        /// </summary>
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
