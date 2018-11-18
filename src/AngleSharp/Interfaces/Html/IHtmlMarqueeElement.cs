namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Attributes;

    /// <summary>
    /// Represents the HTML marquee element.
    /// </summary>
    [DomName("HTMLMarqueeElement")]
    [DomHistorical]
    public interface IHtmlMarqueeElement : IHtmlElement
    {

        /// <summary>
        /// Gets the minimum delay in ms.
        /// </summary>
        Int32 MinimumDelay { get; }

        /// <summary>
        /// Gets or sets the amount of scrolling in pixels.
        /// </summary>
        Int32 ScrollAmount { get; set; }

        /// <summary>
        /// Gets or sets the delay of scrolling in ms.
        /// </summary>
        Int32 ScrollDelay { get; set; }

        /// <summary>
        /// Gets or sets the loop number.
        /// </summary>
        Int32 Loop { get; set; }

        /// <summary>
        /// Starts the marquee loop.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the marquee loop.
        /// </summary>
        void Stop();

    }
}
