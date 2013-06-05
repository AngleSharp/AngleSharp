using System;
namespace AngleSharp.DOM.Html
{
    class HTMLSemanticElement : HTMLElement
    {
        public const string CiteTag = "cite";
        public const string MainTag = "main";
        public const string SummaryTag = "summary";
        public const string XmpTag = "xmp";
        public const string CenterTag = "center";
        public const string ListingTag = "listing";
        public const string NavTag = "nav";
        public const string AddressTag = "address";
        public const string ArticleTag = "article";
        public const string AsideTag = "aside";
        public const string FigcaptionTag = "figcaption";
        public const string FigureTag = "figure";
        public const string SectionTag = "section";
        public const string FooterTag = "footer";
        public const string HeaderTag = "header";
        public const string HgroupTag = "hgroup";
        public const string PlaintextTag = "plaintext";

        bool _special;

        public HTMLSemanticElement(bool special = true)
        {
            _special = special;
        }

        protected internal override bool IsSpecial
        {
            get
            {
                return _special;
            }
        }
    }
}
