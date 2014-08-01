namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Css;
    using AngleSharp.DOM.Navigator;
    using System;

    /// <summary>
    /// The interface for a browsing window element.
    /// https://developer.mozilla.org/en-US/docs/Web/API/Window
    /// </summary>
    [DomName("Window")]
    public interface IWindow : IEventTarget
    {
        /// <summary>
        /// Gives the values of all the CSS properties of an element after
        /// applying the active stylesheets and resolving any basic computation
        /// those values may contain.
        /// </summary>
        /// <param name="element">The element to compute the styles for.</param>
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
        /// Gets the vertical distance of the top border of the user's
        /// browser from the top side of the screen.
        /// </summary>
        [DomName("screenY")]
        Int32 ScreenY { get; }

        /// <summary>
        /// Gets the proxy to the current browsing context.
        /// </summary>
        [DomName("window")]
        [DomName("frames")]
        [DomName("self")]
        IWindowProxy Proxy { get; }

        /// <summary>
        /// Gets the user-agent information.
        /// </summary>
        [DomName("navigator")]
        INavigator Navigator { get; }
    }
}
