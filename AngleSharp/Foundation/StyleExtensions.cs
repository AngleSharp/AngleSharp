namespace AngleSharp
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Collections;
    using System;

    static class StyleExtensions
    {
        /// <summary>
        /// Extends the given style declaration with the set properties of the specified
        /// styling declaration.
        /// </summary>
        /// <param name="style">The declaration to be modified.</param>
        /// <param name="styling">The styling properties to use.</param>
        public static void ExtendWith(this CSSStyleDeclaration style, CSSStyleDeclaration styling)
        {
            foreach (var property in styling)
                style.Set(property);
        }

        /// <summary>
        /// Inherits the unspecified properties from the element's parents.
        /// </summary>
        /// <param name="style">The declaration to be modified.</param>
        /// <param name="element">The element that has unresolved properties.</param>
        public static void InheritFrom(this CSSStyleDeclaration style, Element element)
        {
            //TODO
        }
    }
}
