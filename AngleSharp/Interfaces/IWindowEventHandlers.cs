namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents a set of event handlers for a Window.
    /// </summary>
    [DomName("WindowEventHandlers")]
    interface IWindowEventHandlers
    {
        [DomName("onafterprint")]
        event EventListener Printed;

        [DomName("onbeforeprint")]
        event EventListener Printing;

        [DomName("onbeforeunload")]
        event UnloadEventListener Unloading;

        [DomName("onhashchange")]
        event EventListener HashChanged;

        [DomName("onmessage")]
        event EventListener MessageReceived;

        [DomName("onoffline")]
        event EventListener WentOffline;

        [DomName("ononline")]
        event EventListener WentOnline;

        [DomName("onpagehide")]
        event EventListener PageHidden;

        [DomName("onpageshow")]
        event EventListener PageShown;

        [DomName("onpopstate")]
        event EventListener PopState;

        [DomName("onstorage")]
        event EventListener Storage;

        [DomName("onunload")]
        event EventListener Unloaded;
    }
}
