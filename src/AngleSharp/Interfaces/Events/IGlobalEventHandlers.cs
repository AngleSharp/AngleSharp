namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;

    /// <summary>
    /// Defines a set of event handlers that are implemented by all HTML elements.
    /// </summary>
    [DomName("GlobalEventHandlers")]
    [DomNoInterfaceObject]
    public interface IGlobalEventHandlers
    {
        /// <summary>
        /// Event triggered after aborting.
        /// </summary>
        [DomName("onabort")]
        event DomEventHandler Aborted;

        /// <summary>
        /// Event triggered after losing focus.
        /// </summary>
        [DomName("onblur")]
        event DomEventHandler Blurred;

        /// <summary>
        /// Event triggered after cancelling.
        /// </summary>
        [DomName("oncancel")]
        event DomEventHandler Cancelled;

        /// <summary>
        /// Event triggered when the media can be played.
        /// </summary>
        [DomName("oncanplay")]
        event DomEventHandler CanPlay;

        /// <summary>
        /// Event triggered when the media can be fully played.
        /// </summary>
        [DomName("oncanplaythrough")]
        event DomEventHandler CanPlayThrough;

        /// <summary>
        /// Event triggered after the value changed.
        /// </summary>
        [DomName("onchange")]
        event DomEventHandler Changed;

        /// <summary>
        /// Event triggered after being clicked.
        /// </summary>
        [DomName("onclick")]
        event DomEventHandler Clicked;

        /// <summary>
        /// Event triggered after changing the cue.
        /// </summary>
        [DomName("oncuechange")]
        event DomEventHandler CueChanged;

        /// <summary>
        /// Event triggered after performing a double click.
        /// </summary>
        [DomName("ondblclick")]
        event DomEventHandler DoubleClick;

        /// <summary>
        /// Event triggered after starting to be dragged.
        /// </summary>
        [DomName("ondrag")]
        event DomEventHandler Drag;

        /// <summary>
        /// Event triggered after being dragged.
        /// </summary>
        [DomName("ondragend")]
        event DomEventHandler DragEnd;

        /// <summary>
        /// Event triggered after entering in dragging mode.
        /// </summary>
        [DomName("ondragenter")]
        event DomEventHandler DragEnter;

        /// <summary>
        /// Event triggered after exiting the dragging mode.
        /// </summary>
        [DomName("ondragexit")]
        event DomEventHandler DragExit;

        /// <summary>
        /// Event triggered after leaving in dragging mode.
        /// </summary>
        [DomName("ondragleave")]
        event DomEventHandler DragLeave;

        /// <summary>
        /// Event triggered after hovering in dragging mode.
        /// </summary>
        [DomName("ondragover")]
        event DomEventHandler DragOver;

        /// <summary>
        /// Event triggered after starting to drag.
        /// </summary>
        [DomName("ondragstart")]
        event DomEventHandler DragStart;

        /// <summary>
        /// Event triggered after dropping.
        /// </summary>
        [DomName("ondrop")]
        event DomEventHandler Dropped;

        /// <summary>
        /// Event triggered when the media cursor changed.
        /// </summary>
        [DomName("ondurationchange")]
        event DomEventHandler DurationChanged;

        /// <summary>
        /// Event triggered after being emptied.
        /// </summary>
        [DomName("onemptied")]
        event DomEventHandler Emptied;

        /// <summary>
        /// Event triggered after the media ended.
        /// </summary>
        [DomName("onended")]
        event DomEventHandler Ended;

        /// <summary>
        /// Event triggered after an error occurred.
        /// </summary>
        [DomName("onerror")]
        event DomEventHandler Error;

        /// <summary>
        /// Event triggered after receiving focus.
        /// </summary>
        [DomName("onfocus")]
        event DomEventHandler Focused;

        /// <summary>
        /// Event triggered after input has happend.
        /// </summary>
        [DomName("oninput")]
        event DomEventHandler Input;

        /// <summary>
        /// Event triggered after validation failed.
        /// </summary>
        [DomName("oninvalid")]
        event DomEventHandler Invalid;

        /// <summary>
        /// Event triggered after key down.
        /// </summary>
        [DomName("onkeydown")]
        event DomEventHandler KeyDown;

        /// <summary>
        /// Event triggered after key press.
        /// </summary>
        [DomName("onkeypress")]
        event DomEventHandler KeyPress;

        /// <summary>
        /// Event triggered after key up.
        /// </summary>
        [DomName("onkeyup")]
        event DomEventHandler KeyUp;

        /// <summary>
        /// Event triggered after the content has been fully loaded.
        /// </summary>
        [DomName("onload")]
        event DomEventHandler Loaded;

        /// <summary>
        /// Event triggered after the data has been loaded.
        /// </summary>
        [DomName("onloadeddata")]
        event DomEventHandler LoadedData;

        /// <summary>
        /// Event triggered after the meta data has been received.
        /// </summary>
        [DomName("onloadedmetadata")]
        event DomEventHandler LoadedMetadata;

        /// <summary>
        /// Event triggered when loading begins.
        /// </summary>
        [DomName("onloadstart")]
        event DomEventHandler Loading;

        /// <summary>
        /// Event triggered after mouse down.
        /// </summary>
        [DomName("onmousedown")]
        event DomEventHandler MouseDown;

        /// <summary>
        /// Event triggered after mouse enter.
        /// </summary>
        [DomLenientThis]
        [DomName("onmouseenter")]
        event DomEventHandler MouseEnter;

        /// <summary>
        /// Event triggered after mouse leave.
        /// </summary>
        [DomLenientThis]
        [DomName("onmouseleave")]
        event DomEventHandler MouseLeave;

        /// <summary>
        /// Event triggered after mouse move.
        /// </summary>
        [DomName("onmousemove")]
        event DomEventHandler MouseMove;

        /// <summary>
        /// Event triggered after mouse out.
        /// </summary>
        [DomName("onmouseout")]
        event DomEventHandler MouseOut;

        /// <summary>
        /// Event triggered after mouse over.
        /// </summary>
        [DomName("onmouseover")]
        event DomEventHandler MouseOver;

        /// <summary>
        /// Event triggered after mouse up.
        /// </summary>
        [DomName("onmouseup")]
        event DomEventHandler MouseUp;

        /// <summary>
        /// Event triggered after mouse wheel.
        /// </summary>
        [DomName("onmousewheel")]
        event DomEventHandler MouseWheel;

        /// <summary>
        /// Event triggered after the media paused.
        /// </summary>
        [DomName("onpause")]
        event DomEventHandler Paused;

        /// <summary>
        /// Event triggered after the media started.
        /// </summary>
        [DomName("onplay")]
        event DomEventHandler Played;

        /// <summary>
        /// Event triggered before the media started.
        /// </summary>
        [DomName("onplaying")]
        event DomEventHandler Playing;

        /// <summary>
        /// Event triggered after progress.
        /// </summary>
        [DomName("onprogress")]
        event DomEventHandler Progress;

        /// <summary>
        /// Event triggered after the rate changed.
        /// </summary>
        [DomName("onratechange")]
        event DomEventHandler RateChanged;

        /// <summary>
        /// Event triggered after resetting the form.
        /// </summary>
        [DomName("onreset")]
        event DomEventHandler Resetted;

        /// <summary>
        /// Event triggered after resizing the window.
        /// </summary>
        [DomName("onresize")]
        event DomEventHandler Resized;

        /// <summary>
        /// Event triggered after scrolling has happened.
        /// </summary>
        [DomName("onscroll")]
        event DomEventHandler Scrolled;

        /// <summary>
        /// Event triggered after seeking in the media stream.
        /// </summary>
        [DomName("onseeked")]
        event DomEventHandler Seeked;

        /// <summary>
        /// Event triggered before seeking in the media stream.
        /// </summary>
        [DomName("onseeking")]
        event DomEventHandler Seeking;

        /// <summary>
        /// Event triggered after selected the element.
        /// </summary>
        [DomName("onselect")]
        event DomEventHandler Selected;

        /// <summary>
        /// Event triggered after being shown.
        /// </summary>
        [DomName("onshow")]
        event DomEventHandler Shown;

        /// <summary>
        /// Event triggered after being stalled.
        /// </summary>
        [DomName("onstalled")]
        event DomEventHandler Stalled;

        /// <summary>
        /// Event triggered after the form has been submitted.
        /// </summary>
        [DomName("onsubmit")]
        event DomEventHandler Submitted;

        /// <summary>
        /// Event triggered after suspending.
        /// </summary>
        [DomName("onsuspend")]
        event DomEventHandler Suspended;

        /// <summary>
        /// Event triggered after the time updated.
        /// </summary>
        [DomName("ontimeupdate")]
        event DomEventHandler TimeUpdated;

        /// <summary>
        /// Event triggered after being toggled.
        /// </summary>
        [DomName("ontoggle")]
        event DomEventHandler Toggled;

        /// <summary>
        /// Event triggered after the volume changed.
        /// </summary>
        [DomName("onvolumechange")]
        event DomEventHandler VolumeChanged;

        /// <summary>
        /// Event triggered when waiting for input.
        /// </summary>
        [DomName("onwaiting")]
        event DomEventHandler Waiting;
    }
}
