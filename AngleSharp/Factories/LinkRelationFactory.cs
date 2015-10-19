namespace AngleSharp.Factories
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Html;
    using AngleSharp.Html.LinkRels;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to Relation instance mappings.
    /// </summary>
    sealed class LinkRelationFactory
    {
        delegate BaseLinkRelation Creator(IHtmlLinkElement link);

        readonly Dictionary<String, Creator> creators = new Dictionary<String, Creator>(StringComparer.OrdinalIgnoreCase)
        {
            { LinkRelNames.StyleSheet, link => new StyleSheetLinkRelation(link) },
            { LinkRelNames.Import, link => new ImportLinkRelation(link) },
        };

        /// <summary>
        /// Creates an LinkRel provider for the provided element.
        /// </summary>
        /// <param name="link">The link element.</param>
        /// <param name="rel">The current value of the rel attribute.</param>
        /// <returns>The LinkRel provider instance or null.</returns>
        public BaseLinkRelation Create(IHtmlLinkElement link, String rel)
        {
            var creator = default(Creator);

            if (creators.TryGetValue(rel, out creator))
                return creator(link);

            return null;
        }
    }
}
