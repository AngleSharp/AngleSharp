using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the an HTML heading element (h1, h2, h3, h4, h5, h6).
    /// </summary>
    public sealed class HTMLHeadingElement : HTMLElement
    {
        /// <summary>
        /// The h1 tag.
        /// </summary>
        internal const String ChapterTag = "h1";

        /// <summary>
        /// The h2 tag.
        /// </summary>
        internal const String SectionTag = "h2";

        /// <summary>
        /// The h3 tag.
        /// </summary>
        internal const String SubSectionTag = "h3";

        /// <summary>
        /// The h4 tag.
        /// </summary>
        internal const String SubSubSectionTag = "h4";

        /// <summary>
        /// The h5 tag.
        /// </summary>
        internal const String SubSubSubSectionTag = "h5";

        /// <summary>
        /// The h6 tag.
        /// </summary>
        internal const String SubSubSubSubSectionTag = "h6";

        internal HTMLHeadingElement()
        {
            _name = ChapterTag;
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }
    }
}
