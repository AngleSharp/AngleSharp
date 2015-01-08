namespace AngleSharp.Extensions
{
    using AngleSharp.Css;
    using AngleSharp.DOM;
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Css;
    using AngleSharp.DOM.Html;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// A set of extension methods for style / related methods.
    /// </summary>
    [DebuggerStepThrough]
    static class StyleExtensions
    {
        /// <summary>
        /// Computes the declarations for the given element in the context of
        /// the specified styling rules.
        /// </summary>
        /// <param name="rules">The styles to use.</param>
        /// <param name="element">The element that is questioned.</param>
        /// <returns>The style declaration containing all the declarations.</returns>
        public static CssStyleDeclaration ComputeDeclarations(this StyleCollection rules, IElement element)
        {
            var nodes = element.GetInclusiveAncestorsOf().OfType<IElement>().Reverse().ToArray();
            var bag = new PropertyBag();

            foreach (var node in nodes)
                rules.ComputeStyle(bag, node);

            var htmlElement = element as HTMLElement;

            if (htmlElement != null)
                htmlElement.Style.ApplyTo(bag, Priority.Inline);

            return new CssStyleDeclaration(bag);
        }

        public static void ComputeStyle(this StyleCollection rules, PropertyBag bag, IElement element)
        {
            foreach (var rule in rules)
            {
                var selector = rule.Selector;

                if (selector.Match(element))
                    rule.Style.ApplyTo(bag, selector.Specifity);
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

        /// <summary>
        /// Gets an enumeration over all the stylesheets from the given parent.
        /// </summary>
        /// <param name="parent">The parent to use.</param>
        /// <returns>The enumeration over all stylesheets.</returns>
        public static IEnumerable<IStyleSheet> GetStyleSheets(this INode parent)
        {
            foreach (var child in parent.ChildNodes)
            {
                if (child is IElement)
                {
                    var linkStyle = child as ILinkStyle;

                    if (linkStyle != null)
                    {
                        var sheet = linkStyle.Sheet;

                        if (sheet != null)
                            yield return sheet;
                    }
                    else
                    {
                        foreach (var sheet in child.GetStyleSheets())
                            yield return sheet;
                    }
                }
            }
        }

        /// <summary>
        /// Tries to find the matching namespace url for the given prefix.
        /// </summary>
        /// <param name="sheets">The list of style sheets.</param>
        /// <param name="prefix">The prefix of the namespace to find.</param>
        public static String LocateNamespace(this IStyleSheetList sheets, String prefix)
        {
            foreach (var sheet in sheets)
            {
                var css = sheet as CssStyleSheet;

                if (sheet.IsDisabled || css == null)
                    continue;

                foreach (var rule in css.Rules.OfType<CssNamespaceRule>())
                {
                    if (rule.Prefix == prefix)
                        return rule.NamespaceUri;
                }
            }

            return null;
        }
    }
}
