namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Used to declare properties for the marquee element.
    /// </summary>
    [DomName("HTMLMarqueeElement")]
    public interface IHtmlMarqueeElement
    {
        /// <summary>
        /// Gets the minimum delay in ms.
        /// </summary>
        Int32 MinimumDelay { get; }

        /// <summary>
        /// Gets or sets the amount of scrolling in pixels.
        /// </summary>
        [DomName("scrollamount")]
        Int32 ScrollAmount { get; set; }

        /// <summary>
        /// Gets or sets the delay of scrolling in ms.
        /// </summary>
        [DomName("scrolldelay")]
        Int32 ScrollDelay { get; set; }

        /// <summary>
        /// Gets or sets the loop number.
        /// </summary>
        [DomName("loop")]
        Int32 Loop { get; set; }
    }
}
