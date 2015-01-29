namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Extends the document with further properties for styling.
    /// </summary>
    [DomName("DocumentStyle")]
    public interface IDocumentStyle
    {
        /// <summary>
        /// Gets a list of stylesheet objects for stylesheets explicitly linked
        /// into or embedded in a document.
        /// </summary>
        [DomName("styleSheets")]
        IStyleSheetList StyleSheets { get; }

        /// <summary>
        /// Gets or sets the selected set of stylesheets.
        /// </summary>
        [DomName("selectedStyleSheetSet")]
        String SelectedStyleSheetSet { get; set; }

        /// <summary>
        /// Gets the last stylesheet set.
        /// </summary>
        [DomName("lastStyleSheetSet")]
        String LastStyleSheetSet { get; }

        /// <summary>
        /// Gets the preferred stylesheet set.
        /// </summary>
        [DomName("preferredStyleSheetSet")]
        String PreferredStyleSheetSet { get; }

        /// <summary>
        /// Gets a live list of all of the currently-available style sheet
        /// sets.
        /// </summary>
        [DomName("styleSheetSets")]
        IStringList StyleSheetSets { get; }

        /// <summary>
        /// Enables the stylesheets matching the specified name in the current
        /// style sheet set, and disables all other style sheets (except those
        /// without a title, which are always enabled).
        /// </summary>
        /// <param name="name">The name of the sheets to enable.</param>
        [DomName("enableStyleSheetsForSet")]
        void EnableStyleSheetsForSet(String name);
    }
}
