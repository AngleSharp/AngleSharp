namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML marquee element.
    /// </summary>
    [DomHistorical]
    sealed class HtmlMarqueeElement : HtmlElement
    {
        #region ctor

        public HtmlMarqueeElement(Document owner, String prefix = null)
            : base(owner, Tags.Marquee, prefix, NodeFlags.Special | NodeFlags.Scoped)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the minimum delay in ms.
        /// </summary>
        public Int32 MinimumDelay
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the amount of scrolling in pixels.
        /// </summary>
        public Int32 ScrollAmount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the delay of scrolling in ms.
        /// </summary>
        public Int32 ScrollDelay
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the loop number.
        /// </summary>
        public Int32 Loop
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Starts the marquee loop.
        /// </summary>
        public void Start()
        {
            this.FireSimpleEvent(EventNames.Play);
        }

        /// <summary>
        /// Stops the marquee loop.
        /// </summary>
        public void Stop()
        {
            this.FireSimpleEvent(EventNames.Pause);
        }

        #endregion
    }
}
