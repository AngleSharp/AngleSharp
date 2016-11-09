namespace AngleSharp.Html
{
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.LinkRels;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to Relation instance mappings.
    /// </summary>
    public sealed class DefaultLinkRelationFactory : ILinkRelationFactory
    {
        /// <summary>
        /// Represents a creator delegate for creating link relation providers.
        /// </summary>
        /// <param name="link">The link to create the provider for.</param>
        /// <returns>The created link relation provider.</returns>
        public delegate BaseLinkRelation Creator(IHtmlLinkElement link);

        private readonly Dictionary<String, Creator> _creators = new Dictionary<String, Creator>(StringComparer.OrdinalIgnoreCase)
        {
            { LinkRelNames.StyleSheet, link => new StyleSheetLinkRelation(link) },
            { LinkRelNames.Import, link => new ImportLinkRelation(link) },
        };

        /// <summary>
        /// Registers a new creator for the specified relation.
        /// Throws an exception if another creator for the given
        /// relation is already added.
        /// </summary>
        /// <param name="rel">The relation value.</param>
        /// <param name="creator">The creator to invoke.</param>
        public void Register(String rel, Creator creator)
        {
            _creators.Add(rel, creator);
        }

        /// <summary>
        /// Unregisters an existing creator for the given relation.
        /// </summary>
        /// <param name="rel">The relation value.</param>
        /// <returns>The registered creator, if any.</returns>
        public Creator Unregister(String rel)
        {
            var creator = default(Creator);

            if (_creators.TryGetValue(rel, out creator))
            {
                _creators.Remove(rel);
            }

            return creator;
        }

        /// <summary>
        /// Creates an LinkRel provider for the provided element.
        /// </summary>
        /// <param name="link">The link element.</param>
        /// <param name="rel">The current value of the rel attribute.</param>
        /// <returns>The LinkRel provider instance or null.</returns>
        public BaseLinkRelation Create(IHtmlLinkElement link, String rel)
        {
            var creator = default(Creator);

            if (rel != null && _creators.TryGetValue(rel, out creator))
            {
                return creator.Invoke(link);
            }

            return default(BaseLinkRelation);
        }
    }
}
