namespace AngleSharp.Html.Dom
{
    using AngleSharp.Html;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Defines a set of extension methods for link elements.
    /// </summary>
    public static class HtmlLinkElementExtensions
    {
        #region Linked Stylesheet States

        /// <summary>
        /// Gets if the link contains a stylesheet that is regarded persistent.
        /// </summary>
        /// <param name="link">The link to examine.</param>
        /// <returns>True if the link hosts a persistent stylesheet.</returns>
        public static Boolean IsPersistent(this IHtmlLinkElement link)
        {
            return link.Relation.Isi(LinkRelNames.StyleSheet) && link.Title == null;
        }

        /// <summary>
        /// Gets if the link contains a stylesheet that is regarded preferred.
        /// </summary>
        /// <param name="link">The link to examine.</param>
        /// <returns>True if the link hosts a preferred stylesheet.</returns>
        public static Boolean IsPreferred(this IHtmlLinkElement link)
        {
            return link.Relation.Isi(LinkRelNames.StyleSheet) && link.Title != null;
        }

        /// <summary>
        /// Gets if the link contains a stylesheet that is regarded alternate.
        /// </summary>
        /// <param name="link">The link to examine.</param>
        /// <returns>True if the link hosts an alternate stylesheet.</returns>
        public static Boolean IsAlternate(this IHtmlLinkElement link)
        {
            var relation = link.RelationList;
            return relation.Contains(LinkRelNames.StyleSheet) && relation.Contains(LinkRelNames.Alternate) && link.Title != null;
        }

        #endregion
    }
}
