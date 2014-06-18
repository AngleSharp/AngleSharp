namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Defines a set of event handlers that are implemented by all HTML elements.
    /// </summary>
    [DomName("GlobalEventHandlers")]
    interface IGlobalEventHandlers
    {
        [DomName("onabort")]
        event EventListener Aborted;

        [DomName("onblur")]
        event EventListener Blurred;

        [DomName("oncancel")]
        event EventListener Cancelled;

        [DomName("oncanplay")]
        event EventListener CanPlay;

        [DomName("oncanplaythrough")]
        event EventListener CanPlayThrough;

        [DomName("onchange")]
        event EventListener Changed;

        [DomName("onclick")]
        event EventListener Clicked;

        [DomName("oncuechange")]
        event EventListener CueChanged;

        [DomName("ondblclick")]
        event EventListener DoubleClick;

        [DomName("ondrag")]
        event EventListener Drag;

        [DomName("ondragend")]
        event EventListener DragEnd;

        [DomName("ondragenter")]
        event EventListener DragEnter;

        [DomName("ondragexit")]
        event EventListener DragExit;

        [DomName("ondragleave")]
        event EventListener DragLeave;

        [DomName("ondragover")]
        event EventListener DragOver;

        [DomName("ondragstart")]
        event EventListener DragStart;

        [DomName("ondrop")]
        event EventListener Dropped;

        [DomName("ondurationchange")]
        event EventListener DurationChanged;

        [DomName("onemptied")]
        event EventListener Emptied;

        [DomName("onended")]
        event EventListener Ended;

        [DomName("onfocus")]
        event ErrorEventListener Error;

        [DomName("")]
        event EventListener Focused;

        [DomName("oninput")]
        event EventListener Input;

        [DomName("oninvalid")]
        event EventListener Invalid;

        [DomName("onkeydown")]
        event EventListener KeyDown;

        [DomName("onkeypress")]
        event EventListener KeyPress;

        [DomName("onkeyup")]
        event EventListener KeyUp;

        [DomName("onload")]
        event EventListener Loaded;

        [DomName("onloadeddata")]
        event EventListener LoadedData;

        [DomName("onloadedmetadata")]
        event EventListener LoadedMetadata;

        [DomName("onloadstart")]
        event EventListener Loading;

        [DomName("onmousedown")]
        event EventListener MouseDown;

        [DomName("onmouseenter")]
        event EventListener MouseEnter;

        [DomName("onmouseleave")]
        event EventListener MouseLeave;

        [DomName("onmousemove")]
        event EventListener MouseMove;

        [DomName("onmouseout")]
        event EventListener MouseOut;

        [DomName("onmouseover")]
        event EventListener MouseOver;

        [DomName("onmouseup")]
        event EventListener MouseUp;

        [DomName("onmousewheel")]
        event EventListener MouseWheel;

        [DomName("onpause")]
        event EventListener Paused;

        [DomName("onplay")]
        event EventListener Played;

        [DomName("onplaying")]
        event EventListener Playing;

        [DomName("onprogress")]
        event EventListener Progress;

        [DomName("onratechange")]
        event EventListener RateChanged;

        [DomName("onreset")]
        event EventListener Resetted;

        [DomName("onresize")]
        event EventListener Resized;

        [DomName("onscroll")]
        event EventListener Scrolled;

        [DomName("onseeked")]
        event EventListener Seeked;

        [DomName("onseeking")]
        event EventListener Seeking;

        [DomName("onselect")]
        event EventListener Selected;

        [DomName("onshow")]
        event EventListener Shown;

        [DomName("onstalled")]
        event EventListener Stalled;

        [DomName("onsubmit")]
        event EventListener Submitted;

        [DomName("onsuspend")]
        event EventListener Suspended;

        [DomName("ontimeupdate")]
        event EventListener TimeUpdated;

        [DomName("ontoggle")]
        event EventListener Toggled;

        [DomName("onvolumechange")]
        event EventListener VolumeChanged;

        [DomName("onwaiting")]
        event EventListener Waiting;
    }
}
