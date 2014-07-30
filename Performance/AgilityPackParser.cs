namespace Performance
{
    using HtmlAgilityPack;
    using System;

    class AgilityPackParser : IHtmlParser
    {
        public String Name
        {
            get { return "HTMLAgilityPack"; }
        }

        public void Parse(String source)
        {
            var document = new HtmlDocument();
            document.LoadHtml(source);
        }
    }
}
