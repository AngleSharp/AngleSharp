namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A set of extension methods for style / related methods.
    /// </summary>
    public static class StyleExtensions
    {
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
                {
                    continue;
                }

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
                {
                    continue;
                }
                else if (sheet.IsDisabled)
                {
                    excluded.Add(title);
                }
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
                {
                    sheet.IsDisabled = title != name;
                }
            }
        }

        /// <summary>
        /// Creates a new StyleSheetList instance for the given node.
        /// </summary>
        /// <param name="parent">The node to get the StyleSheets from.</param>
        /// <returns>The new StyleSheetList instance.</returns>
        public static IStyleSheetList CreateStyleSheets(this INode parent)
        {
            var list = parent.GetStyleSheets();
            return new StyleSheetList(list);
        }

        /// <summary>
        /// Creates a new StringList instance with stylesheet sets for the given
        /// node.
        /// </summary>
        /// <param name="parent">The node to get the sets from.</param>
        /// <returns>The new StringList instance.</returns>
        public static IStringList CreateStyleSheetSets(this INode parent)
        {
            var list = parent.GetStyleSheets().Select(m => m.Title).Where(m => m != null);
            return new StringList(list);
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
                if (child.NodeType == NodeType.Element)
                {
                    if (child is ILinkStyle linkStyle)
                    {
                        var sheet = linkStyle.Sheet;

                        if (sheet != null && !sheet.IsDisabled)
                        {
                            yield return sheet;
                        }
                    }
                    else
                    {
                        foreach (var sheet in child.GetStyleSheets())
                        {
                            yield return sheet;
                        }
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
            var uri = default(String);
            var length = sheets.Length;

            for (var i = 0; i < length && uri == null; i++)
            {
                uri = sheets[i].LocateNamespace(prefix);
            }

            return uri;
        }
    }
}
