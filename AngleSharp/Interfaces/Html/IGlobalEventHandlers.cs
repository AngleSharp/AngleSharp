namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Defines a set of event handlers that are implemented by all HTML elements.
    /// </summary>
    [DomName("GlobalEventHandlers")]
    public interface IGlobalEventHandlers
    {
        /// <summary>
        /// Event triggered after aborting.
        /// </summary>
        [DomName("onabort")]
        event EventListener Aborted;

        /// <summary>
        /// Event triggered after losing focus.
        /// </summary>
        [DomName("onblur")]
        event EventListener Blurred;

        /// <summary>
        /// Event triggered after cancelling.
        /// </summary>
        [DomName("oncancel")]
        event EventListener Cancelled;

        /// <summary>
        /// Event triggered when the media can be played.
        /// </summary>
        [DomName("oncanplay")]
        event EventListener CanPlay;

        /// <summary>
        /// Event triggered when the media can be fully played.
        /// </summary>
        [DomName("oncanplaythrough")]
        event EventListener CanPlayThrough;

        /// <summary>
        /// Event triggered after the value changed.
        /// </summary>
        [DomName("onchange")]
        event EventListener Changed;

        /// <summary>
        /// Event triggered after being clicked.
        /// </summary>
        [DomName("onclick")]
        event EventListener Clicked;

        /// <summary>
        /// Event triggered after changing the cue.
        /// </summary>
        [DomName("oncuechange")]
        event EventListener CueChanged;

        /// <summary>
        /// Event triggered after performing a double click.
        /// </summary>
        [DomName("ondblclick")]
        event EventListener DoubleClick;

        /// <summary>
        /// Event triggered after starting to be dragged.
        /// </summary>
        [DomName("ondrag")]
        event EventListener Drag;

        /// <summary>
        /// Event triggered after being dragged.
        /// </summary>
        [DomName("ondragend")]
        event EventListener DragEnd;

        /// <summary>
        /// Event triggered after entering in dragging mode.
        /// </summary>
        [DomName("ondragenter")]
        event EventListener DragEnter;

        /// <summary>
        /// Event triggered after exiting the dragging mode.
        /// </summary>
        [DomName("ondragexit")]
        event EventListener DragExit;

        /// <summary>
        /// Event triggered after leaving in dragging mode.
        /// </summary>
        [DomName("ondragleave")]
        event EventListener DragLeave;

        /// <summary>
        /// Event triggered after hovering in dragging mode.
        /// </summary>
        [DomName("ondragover")]
        event EventListener DragOver;

        /// <summary>
        /// Event triggered after starting to drag.
        /// </summary>
        [DomName("ondragstart")]
        event EventListener DragStart;

        /// <summary>
        /// Event triggered after dropping.
        /// </summary>
        [DomName("ondrop")]
        event EventListener Dropped;

        /// <summary>
        /// Event triggered when the media cursor changed.
        /// </summary>
        [DomName("ondurationchange")]
        event EventListener DurationChanged;

        /// <summary>
        /// Event triggered after being emptied.
        /// </summary>
        [DomName("onemptied")]
        event EventListener Emptied;

        /// <summary>
        /// Event triggered after the media ended.
        /// </summary>
        [DomName("onended")]
        event EventListener Ended;

        /// <summary>
        /// Event triggered after an error occurred.
        /// </summary>
        [DomName("onerror")]
        event ErrorEventListener Error;

        /// <summary>
        /// Event triggered after receiving focus.
        /// </summary>
        [DomName("onfocus")]
        event EventListener Focused;

        /// <summary>
        /// Event triggered after input has happend.
        /// </summary>
        [DomName("oninput")]
        event EventListener Input;

        /// <summary>
        /// Event triggered after validation failed.
        /// </summary>
        [DomName("oninvalid")]
        event EventListener Invalid;

        /// <summary>
        /// Event triggered after key down.
        /// </summary>
        [DomName("onkeydown")]
        event EventListener KeyDown;

        /// <summary>
        /// Event triggered after key press.
        /// </summary>
        [DomName("onkeypress")]
        event EventListener KeyPress;

        /// <summary>
        /// Event triggered after key up.
        /// </summary>
        [DomName("onkeyup")]
        event EventListener KeyUp;

        /// <summary>
        /// Event triggered after the content has been fully loaded.
        /// </summary>
        [DomName("onload")]
        event EventListener Loaded;

        /// <summary>
        /// Event triggered after the data has been loaded.
        /// </summary>
        [DomName("onloadeddata")]
        event EventListener LoadedData;

        /// <summary>
        /// Event triggered after the meta data has been received.
        /// </summary>
        [DomName("onloadedmetadata")]
        event EventListener LoadedMetadata;

        /// <summary>
        /// Event triggered when loading begins.
        /// </summary>
        [DomName("onloadstart")]
        event EventListener Loading;

        /// <summary>
        /// Event triggered after mouse down.
        /// </summary>
        [DomName("onmousedown")]
        event EventListener MouseDown;

        /// <summary>
        /// Event triggered after mouse enter.
        /// </summary>
        [DomName("onmouseenter")]
        event EventListener MouseEnter;

        /// <summary>
        /// Event triggered after mouse leave.
        /// </summary>
        [DomName("onmouseleave")]
        event EventListener MouseLeave;

        /// <summary>
        /// Event triggered after mouse move.
        /// </summary>
        [DomName("onmousemove")]
        event EventListener MouseMove;

        /// <summary>
        /// Event triggered after mouse out.
        /// </summary>
        [DomName("onmouseout")]
        event EventListener MouseOut;

        /// <summary>
        /// Event triggered after mouse over.
        /// </summary>
        [DomName("onmouseover")]
        event EventListener MouseOver;

        /// <summary>
        /// Event triggered after mouse up.
        /// </summary>
        [DomName("onmouseup")]
        event EventListener MouseUp;

        /// <summary>
        /// Event triggered after mouse wheel.
        /// </summary>
        [DomName("onmousewheel")]
        event EventListener MouseWheel;

        /// <summary>
        /// Event triggered after the media paused.
        /// </summary>
        [DomName("onpause")]
        event EventListener Paused;

        /// <summary>
        /// Event triggered after the media started.
        /// </summary>
        [DomName("onplay")]
        event EventListener Played;

        /// <summary>
        /// Event triggered before the media started.
        /// </summary>
        [DomName("onplaying")]
        event EventListener Playing;

        /// <summary>
        /// Event triggered after progress.
        /// </summary>
        [DomName("onprogress")]
        event EventListener Progress;

        /// <summary>
        /// Event triggered after the rate changed.
        /// </summary>
        [DomName("onratechange")]
        event EventListener RateChanged;

        /// <summary>
        /// Event triggered after resetting the form.
        /// </summary>
        [DomName("onreset")]
        event EventListener Resetted;

        /// <summary>
        /// Event triggered after resizing the window.
        /// </summary>
        [DomName("onresize")]
        event EventListener Resized;

        /// <summary>
        /// Event triggered after scrolling has happened.
        /// </summary>
        [DomName("onscroll")]
        event EventListener Scrolled;

        /// <summary>
        /// Event triggered after seeking in the media stream.
        /// </summary>
        [DomName("onseeked")]
        event EventListener Seeked;

        /// <summary>
        /// Event triggered before seeking in the media stream.
        /// </summary>
        [DomName("onseeking")]
        event EventListener Seeking;

        /// <summary>
        /// Event triggered after selected the element.
        /// </summary>
        [DomName("onselect")]
        event EventListener Selected;

        /// <summary>
        /// Event triggered after being shown.
        /// </summary>
        [DomName("onshow")]
        event EventListener Shown;

        /// <summary>
        /// Event triggered after being stalled.
        /// </summary>
        [DomName("onstalled")]
        event EventListener Stalled;

        /// <summary>
        /// Event triggered after the form has been submitted.
        /// </summary>
        [DomName("onsubmit")]
        event EventListener Submitted;

        /// <summary>
        /// Event triggered after suspending.
        /// </summary>
        [DomName("onsuspend")]
        event EventListener Suspended;

        /// <summary>
        /// Event triggered after the time updated.
        /// </summary>
        [DomName("ontimeupdate")]
        event EventListener TimeUpdated;

        /// <summary>
        /// Event triggered after being toggled.
        /// </summary>
        [DomName("ontoggle")]
        event EventListener Toggled;

        /// <summary>
        /// Event triggered after the volume changed.
        /// </summary>
        [DomName("onvolumechange")]
        event EventListener VolumeChanged;

        /// <summary>
        /// Event triggered when waiting for input.
        /// </summary>
        [DomName("onwaiting")]
        event EventListener Waiting;
    }
}
