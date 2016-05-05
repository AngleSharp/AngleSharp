namespace AngleSharp.Dom.Media
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a text track cue.
    /// </summary>
    [DomName("TextTrackCue")]
    public interface ITextTrackCue : IEventTarget
    {
        /// <summary>
        /// Gets the text track cue identifier.
        /// </summary>
        [DomName("id")]
        String Id { get; set; }

        /// <summary>
        /// Gets the assigned track for this cue.
        /// </summary>
        [DomName("track")]
        ITextTrack Track { get; }

        /// <summary>
        /// Gets or sets the text track cue start time, in seconds.
        /// </summary>
        [DomName("startTime")]
        Double StartTime { get; set; }

        /// <summary>
        /// Gets or sets the text track cue end time, in seconds.
        /// </summary>
        [DomName("endTime")]
        Double EndTime { get; set; }

        /// <summary>
        /// Gets or sets the text track cue pause-on-exit flag.
        /// </summary>
        [DomName("pauseOnExit")]
        Boolean IsPausedOnExit { get; set; }

        /// <summary>
        /// Gets or sets a string representing the text track cue writing direction,
        /// as follows. If it is horizontal: The empty string. If it is vertical
        /// growing left: The string "rl". If it is vertical growing right: The string "lr".
        /// </summary>
        [DomName("vertical")]
        String Vertical { get; set; }

        /// <summary>
        /// Gets or sets the text track cue snap-to-lines flag.
        /// </summary>
        [DomName("snapToLines")]
        Boolean IsSnappedToLines { get; set; }

        /// <summary>
        /// Gets or sets the text track cue line position. In the case of
        /// the value being auto, the string "auto" is returned.
        /// </summary>
        [DomName("line")]
        Int32 Line { get; set; }

        /// <summary>
        /// Gets or sets the text track cue text position.
        /// </summary>
        [DomName("position")]
        Int32 Position { get; set; }

        /// <summary>
        /// Gets or sets the text track cue size.
        /// </summary>
        [DomName("size")]
        Int32 Size { get; set; }

        /// <summary>
        /// Gets or sets a string representing the text track cue alignment, as
        /// follows. If it is start alignment: the string "start". If it is middle
        /// alignment: the string "middle". If it is end alignment: the string "end".
        /// If it is left alignment: the string "left". If it is right alignment:
        /// the string "right".
        /// </summary>
        [DomName("align")]
        String Alignment { get; set; }

        /// <summary>
        /// Gets or sets the text track cue text in raw unparsed form.
        /// </summary>
        [DomName("text")]
        String Text { get; set; }

        /// <summary>
        /// Returns the text track cue text as a DocumentFragment of HTML elements
        /// and other DOM nodes.
        /// </summary>
        /// <returns>The document fragment.</returns>
        [DomName("getCueAsHTML")]
        IDocumentFragment AsHtml();

        /// <summary>
        /// Event triggered after entering.
        /// </summary>
        [DomName("onenter")]
        DomEventHandler Entered { get; set; }

        /// <summary>
        /// Event triggered after exiting.
        /// </summary>
        [DomName("onexit")]
        DomEventHandler Exited { get; set; }
    }
}
