namespace AngleSharp
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Collections;
    using System;

    /// <summary>
    /// The interface for a browsing window element.
    /// https://developer.mozilla.org/en-US/docs/Web/API/Window
    /// </summary>
    public interface IWindow
    {
        /// <summary>
        /// Gives the values of all the CSS properties of an element after
        /// applying the active stylesheets and resolving any basic computation
        /// those values may contain.
        /// </summary>
        /// <param name="element">The element to compute the styles for.</param>
        /// <param name="pseudo">The optional pseudo selector to use.</param>
        /// <returns>The style declaration describing the element.</returns>
        [DOM("getComputedStyle")]
        CSSStyleDeclaration GetComputedStyle(Element element, String pseudo = null);

        /// <summary>
        /// Gets a reference to the document that the window contains.
        /// </summary>
        [DOM("document")]
        Document Document { get; }

        /// <summary>
        /// Gets or sets the name of the window.
        /// </summary>
        [DOM("name")]
        String Name { get; set; }

        /// <summary>
        /// Gets the height of the outside of the browser window.
        /// </summary>
        [DOM("outerHeight")]
        Int32 OuterHeight { get; }

        /// <summary>
        /// Gets the width of the outside of the browser window.
        /// </summary>
        [DOM("outerWidth")]
        Int32 OuterWidth { get; }

        /// <summary>
        /// Gets the horizontal distance of the left border of the user's
        /// browser from the left side of the screen.
        /// </summary>
        [DOM("screenX")]
        Int32 ScreenX { get; }

        /// <summary>
        /// Gets the vertical distance of the top border of the user's
        /// browser from the top side of the screen.
        /// </summary>
        [DOM("screenY")]
        Int32 ScreenY { get; }
    }
}
