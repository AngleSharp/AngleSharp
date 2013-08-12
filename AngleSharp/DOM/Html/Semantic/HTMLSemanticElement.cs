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
        internal const String CiteTag = "cite";

        /// <summary>
        /// The main tag.
        /// </summary>
        internal const String MainTag = "main";

        /// <summary>
        /// The summary tag.
        /// </summary>
        internal const String SummaryTag = "summary";

        /// <summary>
        /// The xmp tag.
        /// </summary>
        internal const String XmpTag = "xmp";

        /// <summary>
        /// The center tag.
        /// </summary>
        internal const String CenterTag = "center";

        /// <summary>
        /// The listing tag.
        /// </summary>
        internal const String ListingTag = "listing";

        /// <summary>
        /// The nav tag.
        /// </summary>
        internal const String NavTag = "nav";

        /// <summary>
        /// The address tag.
        /// </summary>
        internal const String AddressTag = "address";

        /// <summary>
        /// The article tag.
        /// </summary>
        internal const String ArticleTag = "article";

        /// <summary>
        /// The aside tag.
        /// </summary>
        internal const String AsideTag = "aside";

        /// <summary>
        /// The figcaption tag.
        /// </summary>
        internal const String FigcaptionTag = "figcaption";

        /// <summary>
        /// The figure tag.
        /// </summary>
        internal const String FigureTag = "figure";

        /// <summary>
        /// The section tag.
        /// </summary>
        internal const String SectionTag = "section";

        /// <summary>
        /// The footer tag.
        /// </summary>
        internal const String FooterTag = "footer";

        /// <summary>
        /// The header tag.
        /// </summary>
        internal const String HeaderTag = "header";

        /// <summary>
        /// The hgroup tag.
        /// </summary>
        internal const String HgroupTag = "hgroup";

        /// <summary>
        /// The plaintext tag.
        /// </summary>
        internal const String PlaintextTag = "plaintext";

        #endregion

        #region Members

        Boolean _special;

        #endregion

        #region ctor

        internal HTMLSemanticElement(Boolean special = true)
        {
            _special = special;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return _special; }
        }

        #endregion
    }
}
