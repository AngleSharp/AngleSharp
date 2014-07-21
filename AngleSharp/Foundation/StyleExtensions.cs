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
        /// <param name="inherit">True if styling is from parent element.</param>
        public static void ExtendWith(this CSSStyleDeclaration style, CSSStyleDeclaration styling, Boolean inherit = false)
        {
            foreach (var property in styling)
            {
                var styleProperty = style.Get(property.Name);

                // Check if property should be inherited and if set yet.
                if (inherit) 
                {
                    var newProperty = property.Clone();

                    if (styleProperty == null || ((!styleProperty.Important || newProperty.Important) && !styleProperty.IsInherited))
                        style.Set(newProperty);

                    continue;
                }

                // property of style is not set or not important, or property is important
                if (styleProperty == null || !styleProperty.Important || property.Important)
                    style.Set(property);
            }
        }

        /// <summary>
        /// Inherits the unspecified properties from the element's parents.
        /// </summary>
        /// <param name="style">The declaration to be modified.</param>
        /// <param name="element">The element that has unresolved properties.</param>
        /// <param name="window">The associated window object.</param>
        public static void InheritFrom(this CSSStyleDeclaration style, IElement element, IWindow window)
        {
            var parent = element.ParentElement;

            if (parent != null)
                style.ExtendWith(window.GetComputedStyle(parent), true);
        }
    }
}
