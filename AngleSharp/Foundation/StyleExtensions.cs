namespace AngleSharp
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Css;

    static class StyleExtensions
    {
        /// <summary>
        /// Extends the given style declaration with the set properties of the specified
        /// styling declaration.
        /// </summary>
        /// <param name="style">The declaration to be modified.</param>
        /// <param name="styling">The styling properties to use.</param>
        /// <param name="priority">Sets the priority of the new properties.</param>
        public static void ExtendWith(this CSSStyleDeclaration style, ICssStyleDeclaration styling, Priority priority)
        {
            foreach (var property in styling)
            {
                var styleProperty = style.Get(property.Name);

                // property of style is not set or not important, or property is important
                style.Set(property, priority);
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
            {
                var styling = window.GetComputedStyle(parent);

                foreach (var property in styling)
                {
                    var styleProperty = style.Get(property.Name);

                    if (styleProperty == null || styleProperty.IsInherited)
                        style.Set(property);
                }
            }
        }
    }
}
