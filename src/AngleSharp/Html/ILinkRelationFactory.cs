namespace AngleSharp.Html
{
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.LinkRels;
    using System;

    /// <summary>
    /// Represents the factory for resolving link relation.
    /// </summary>
    public interface ILinkRelationFactory
    {
        /// <summary>
        /// Creates an link relation provider for the given element.
        /// </summary>
        /// <param name="link">The link element.</param>
        /// <param name="relation">The current value of the rel attribute.</param>
        /// <returns>The provider instance or null.</returns>
        BaseLinkRelation Create(IHtmlLinkElement link, String relation);
    }
}
