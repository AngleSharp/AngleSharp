namespace AngleSharp
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Css;

    static class StyleExtensions
    {
        /// <summary>
        /// Extends the given bag with the set properties of the specified
        /// styling declaration.
        /// </summary>
        /// <param name="bag">The bag to modify.</param>
        /// <param name="styling">The styling properties to use.</param>
        /// <param name="priority">Sets the priority of the new properties.</param>
        public static void ExtendWith(this CssPropertyBag bag, ICssStyleDeclaration styling, Priority priority)
        {
            foreach (var property in styling)
                bag.TryUpdate(property, priority);
        }

        /// <summary>
        /// Inherits the unspecified properties from the element's parents.
        /// </summary>
        /// <param name="bag">The bag to modify.</param>
        /// <param name="element">The element that has unresolved properties.</param>
        /// <param name="window">The associated window object.</param>
        public static void InheritFrom(this CssPropertyBag bag, IElement element, IWindow window)
        {
            var parent = element.ParentElement;

            if (parent != null)
            {
                var styling = window.GetComputedStyle(parent);

                foreach (var property in styling)
                {
                    var styleProperty = bag[property.Name];

                    if (styleProperty == null || styleProperty.IsInherited)
                        bag.TryUpdate(property);
                }
            }
        }
    }
}
