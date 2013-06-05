using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLHeadingElement : HTMLElement
    {
        public const string ChapterTag = "h1";
        public const string SectionTag = "h2";
        public const string SubSectionTag = "h3";
        public const string SubSubSectionTag = "h4";
        public const string SubSubSubSectionTag = "h5";
        public const string SubSubSubSubSectionTag = "h6";

        public HTMLHeadingElement()
        {
            NodeName = ChapterTag;
        }

        protected internal override bool IsSpecial
        {
            get
            {
                return true;
            }
        }
    }
}
