namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the mod HTML element.
    /// </summary>
    [DomName("HTMLModElement")]
    public interface IHtmlModElement : IHtmlElement
    {
        /// <summary>
        /// Gets the cite HTML attribute, containing a URI of a
        /// resource explaining the change.
        /// </summary>
        [DomName("cite")]
        String Citation { get; set; }

        /// <summary>
        /// Gets the datetime HTML attribute, containing a date-and-time
        /// string representing a timestamp for the change.
        /// </summary>
        [DomName("datetime")]
        String DateTime { get; set; }
    }
}
