using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents an HTML element with only semantic meaning.
    /// </summary>
    sealed class HTMLSemanticElement : HTMLElement
    {
        #region Constants

        /// <summary>
        /// The cite tag.
        /// </summary>
        internal const string CiteTag = "cite";

        /// <summary>
        /// The main tag.
        /// </summary>
        internal const string MainTag = "main";

        /// <summary>
        /// The summary tag.
        /// </summary>
        internal const string SummaryTag = "summary";

        /// <summary>
        /// The xmp tag.
        /// </summary>
        internal const string XmpTag = "xmp";

        /// <summary>
        /// The center tag.
        /// </summary>
        internal const string CenterTag = "center";

        /// <summary>
        /// The listing tag.
        /// </summary>
        internal const string ListingTag = "listing";

        /// <summary>
        /// The nav tag.
        /// </summary>
        internal const string NavTag = "nav";

        /// <summary>
        /// The address tag.
        /// </summary>
        internal const string AddressTag = "address";

        /// <summary>
        /// The article tag.
        /// </summary>
        internal const string ArticleTag = "article";

        /// <summary>
        /// The aside tag.
        /// </summary>
        internal const string AsideTag = "aside";

        /// <summary>
        /// The figcaption tag.
        /// </summary>
        internal const string FigcaptionTag = "figcaption";

        /// <summary>
        /// The figure tag.
        /// </summary>
        internal const string FigureTag = "figure";

        /// <summary>
        /// The section tag.
        /// </summary>
        internal const string SectionTag = "section";

        /// <summary>
        /// The footer tag.
        /// </summary>
        internal const string FooterTag = "footer";

        /// <summary>
        /// The header tag.
        /// </summary>
        internal const string HeaderTag = "header";

        /// <summary>
        /// The hgroup tag.
        /// </summary>
        internal const string HgroupTag = "hgroup";

        /// <summary>
        /// The plaintext tag.
        /// </summary>
        internal const string PlaintextTag = "plaintext";

        #endregion

        #region Members

        bool _special;

        #endregion

        #region ctor

        internal HTMLSemanticElement(bool special = true)
        {
            _special = special;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return _special; }
        }

        #endregion
    }
}
