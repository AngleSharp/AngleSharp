namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;

    /// <summary>
    /// Represents a set of event handlers for a Window.
    /// </summary>
    [DomName("WindowEventHandlers")]
    [DomNoInterfaceObject]
    public interface IWindowEventHandlers
    {
        /// <summary>
        /// Event triggered after printing.
        /// </summary>
        [DomName("onafterprint")]
        event DomEventHandler Printed;

        /// <summary>
        /// Event triggered before printing.
        /// </summary>
        [DomName("onbeforeprint")]
        event DomEventHandler Printing;

        /// <summary>
        /// Event triggered before closing the document.
        /// </summary>
        [DomName("onbeforeunload")]
        event DomEventHandler Unloading;

        /// <summary>
        /// Event triggered when the hash has been changed.
        /// </summary>
        [DomName("onhashchange")]
        event DomEventHandler HashChanged;

        /// <summary>
        /// Event triggered after receiving a message.
        /// </summary>
        [DomName("onmessage")]
        event DomEventHandler MessageReceived;

        /// <summary>
        /// Event triggered after losing connection.
        /// </summary>
        [DomName("onoffline")]
        event DomEventHandler WentOffline;

        /// <summary>
        /// Event triggered after being connected.
        /// </summary>
        [DomName("ononline")]
        event DomEventHandler WentOnline;

        /// <summary>
        /// Event triggered after the page has been hidden.
        /// </summary>
        [DomName("onpagehide")]
        event DomEventHandler PageHidden;

        /// <summary>
        /// Event triggered after showing the page.
        /// </summary>
        [DomName("onpageshow")]
        event DomEventHandler PageShown;

        /// <summary>
        /// Event triggered after popping the state from the history.
        /// </summary>
        [DomName("onpopstate")]
        event DomEventHandler PopState;

        /// <summary>
        /// Event triggered after using the provided storage.
        /// </summary>
        [DomName("onstorage")]
        event DomEventHandler Storage;

        /// <summary>
        /// Event triggered after after closing the document.
        /// </summary>
        [DomName("onunload")]
        event DomEventHandler Unloaded;
    }
}
