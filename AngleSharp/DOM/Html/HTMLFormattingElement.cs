using System;

namespace AngleSharp.DOM.Html
{
    public class HTMLFormattingElement : HTMLElement
    {
        public const string BTag = "b";
        public const string BigTag = "big";
        public const string StrikeTag = "strike";
        public const string CodeTag = "code";
        public const string EmTag = "em";
        public const string ITag = "i";
        public const string STag = "s";
        public const string SmallTag = "small";
        public const string StrongTag = "strong";
        public const string UTag = "u";
        public const string TtTag = "tt";
        public const string NobrTag = "nobr";

        protected internal override bool IsSpecial
        {
            get
            {
                return false;
            }
        }
    }
}
