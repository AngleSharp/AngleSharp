namespace AngleSharp
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Collections;
    using System;

    static class StyleExtensions
    {
        /// <summary>
        /// Extends the given style declaration with the set properties of the specified
        /// styling declaration. Computes the values in the context of the specified
        /// window.
        /// </summary>
        /// <param name="style">The declaration to be extended.</param>
        /// <param name="styling">The styling properties to use.</param>
        /// <param name="window">The browsing context to use.</param>
        public static void Extend(this CSSStyleDeclaration style, CSSStyleDeclaration styling, IWindow window)
        {
            //TODO
        }

        /// <summary>
        /// Inherits the unspecified properties from the element's parents.
        /// </summary>
        /// <param name="style"></param>
        /// <param name="element"></param>
        /// <param name="window"></param>
        public static void Inherit(this CSSStyleDeclaration style, Element element, IWindow window)
        {
            //TODO
        }
    }
}
