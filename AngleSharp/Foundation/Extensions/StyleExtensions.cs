namespace AngleSharp
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

        /// <summary>
        /// Gets all possible style sheet sets from the list of style sheets.
        /// </summary>
        /// <param name="sheets">The list of style sheets.</param>
        /// <returns>An enumeration over all sets.</returns>
        public static IEnumerable<String> GetAllStyleSheetSets(this IStyleSheetList sheets)
        {
            var existing = new List<String>();

            foreach (var sheet in sheets)
            {
                var title = sheet.Title;

                if (String.IsNullOrEmpty(title) || existing.Contains(title))
                    continue;

                existing.Add(title);
                yield return title;
            }
        }

        /// <summary>
        /// Gets the enabled style sheet sets from the list of style sheets.
        /// </summary>
        /// <param name="sheets">The list of style sheets.</param>
        /// <returns>An enumeration over the enabled sets.</returns>
        public static IEnumerable<String> GetEnabledStyleSheetSets(this IStyleSheetList sheets)
        {
            var excluded = new List<String>();

            foreach (var sheet in sheets)
            {
                var title = sheet.Title;

                if (String.IsNullOrEmpty(title) || excluded.Contains(title))
                    continue;
                else if (sheet.IsDisabled)
                    excluded.Add(title);
            }

            return sheets.GetAllStyleSheetSets().Except(excluded);
        }

        /// <summary>
        /// Sets the enabled style sheet sets in the list of style sheets.
        /// </summary>
        /// <param name="sheets">The list of style sheets.</param>
        /// <param name="name">The name of the set to enabled.</param>
        public static void EnableStyleSheetSet(this IStyleSheetList sheets, String name)
        {
            foreach (var sheet in sheets)
            {
                var title = sheet.Title;

                if (!String.IsNullOrEmpty(title))
                    sheet.IsDisabled = title != name;
            }
        }
    }
}
