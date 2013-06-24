using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML marquee element.
    /// </summary>
    public sealed class HTMLMarqueeElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The marquee tag.
        /// </summary>
        internal const string Tag = "marquee";

        #endregion

        #region ctor

        internal HTMLMarqueeElement()
        {
            _name = Tag;
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
            //TODO
        }

        /// <summary>
        /// Stops the marquee loop.
        /// </summary>
        public void Stop()
        {
            //TODO
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
