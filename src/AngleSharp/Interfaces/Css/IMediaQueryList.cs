namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// A MediaQueryList object maintains a list of media queries on a document,
    /// and handles sending notifications to listeners when the media queries on
    /// the document change.
    /// </summary>
    [DomName("MediaQueryList")]
    public interface IMediaQueryList : IEventTarget
    {
        /// <summary>
        /// Gets the text representation of the underlying media list.
        /// </summary>
        [DomName("media")]
        String MediaText { get; }

        /// <summary>
        /// Gets the associated media list.
        /// </summary>
        IMediaList Media { get; }

        /// <summary>
        /// Gets the current status of the media query.
        /// </summary>
        [DomName("matches")]
        Boolean IsMatched { get; }

        /// <summary>
        /// Event triggered after the value changed.
        /// </summary>
        [DomName("onchange")]
        event DomEventHandler Changed;
    }
}
