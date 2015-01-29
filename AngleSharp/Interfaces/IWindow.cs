namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom.Css;
    using AngleSharp.Dom.Events;
    using AngleSharp.Dom.Navigator;
    using System;

    /// <summary>
    /// The interface for a browsing window element.
    /// https://developer.mozilla.org/en-US/docs/Web/API/Window
    /// </summary>
    [DomName("Window")]
    public interface IWindow : IEventTarget, IGlobalEventHandlers, IWindowEventHandlers, IWindowTimers
    {
        /// <summary>
        /// Gives the values of all the CSS properties of an element after
        /// applying the active stylesheets and resolving any basic computation
        /// those values may contain.
        /// </summary>
        /// <param name="element">
        /// The element to compute the styles for.
        /// </param>
        /// <param name="pseudo">The optional pseudo selector to use.</param>
        /// <returns>The style declaration describing the element.</returns>
        [DomName("getComputedStyle")]
        ICssStyleDeclaration GetComputedStyle(IElement element, String pseudo = null);

        /// <summary>
        /// Gets a reference to the document that the window contains.
        /// </summary>
        [DomName("document")]
        IDocument Document { get; }

        /// <summary>
        /// Gets the location of the current document.
        /// </summary>
        [DomName("location")]
        [DomPutForwards("href")]
        ILocation Location { get; }

        /// <summary>
        /// Gets if the window has been closed.
        /// </summary>
        [DomName("closed")]
        Boolean IsClosed { get; }

        /// <summary>
        /// Gets or sets the status of the window.
        /// </summary>
        [DomName("status")]
        String Status { get; set; }

        /// <summary>
        /// Gets or sets the name of the window.
        /// </summary>
        [DomName("name")]
        String Name { get; set; }

        /// <summary>
        /// Gets the height of the outside of the browser window.
        /// </summary>
        [DomName("outerHeight")]
        Int32 OuterHeight { get; }

        /// <summary>
        /// Gets the width of the outside of the browser window.
        /// </summary>
        [DomName("outerWidth")]
        Int32 OuterWidth { get; }

        /// <summary>
        /// Gets the horizontal distance of the left border of the user's
        /// browser from the left side of the screen.
        /// </summary>
        [DomName("screenX")]
        Int32 ScreenX { get; }

        /// <summary>
        /// Gets the vertical distance of the top border of the user's browser
        /// from the top side of the screen.
        /// </summary>
        [DomName("screenY")]
        Int32 ScreenY { get; }

        /// <summary>
        /// Gets the proxy to the current browsing context.
        /// </summary>
        [DomName("window")]
        [DomName("frames")]
        [DomName("self")]
        IWindow Proxy { get; }

        /// <summary>
        /// Gets the user-agent information.
        /// </summary>
        [DomName("navigator")]
        INavigator Navigator { get; }

        /// <summary>
        /// Closes the window.
        /// </summary>
        [DomName("close")]
        void Close();

        /// <summary>
        /// Opens a window to show url, and returns it. If a window exists with
        /// the provided name already, it is reused.
        /// </summary>
        /// <param name="url">The URL to open initially.</param>
        /// <param name="name">The name of the new window.</param>
        /// <param name="features">
        /// Determines the rendering of the new window.
        /// </param>
        /// <param name="replace">
        /// Can be used to remove whatever page is currently open from the
        /// session history.
        /// </param>
        /// <returns>The new or reused window.</returns>
        IWindow Open(String url = "about:blank", String name = null, String features = null, String replace = null);

        /// <summary>
        /// Stops the current document from being loaded.
        /// </summary>
        [DomName("stop")]
        void Stop();

        /// <summary>
        /// Focuses the current window.
        /// </summary>
        [DomName("focus")]
        void Focus();

        /// <summary>
        /// Removes the focus from the current window.
        /// </summary>
        [DomName("blur")]
        void Blur();

        /// <summary>
        /// Shows the messagebox with the given message.
        /// </summary>
        /// <param name="message">The message to display.</param>
        [DomName("alert")]
        void Alert(String message);

        /// <summary>
        /// Opens a confirmation box with the provided message.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <returns>
        /// True if the message has been confirmed, otherwise false.
        /// </returns>
        [DomName("confirm")]
        Boolean Confirm(String message);

        /// <summary>
        /// Opens the print dialog for the current window.
        /// </summary>
        [DomName("print")]
        void Print();

        /// <summary>
        /// Gets the history of the current window.
        /// </summary>
        [DomName("history")]
        IHistory History { get; }
    }
}
