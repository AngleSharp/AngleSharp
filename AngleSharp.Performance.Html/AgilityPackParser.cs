namespace AngleSharp.Performance.Html
{
    using HtmlAgilityPack;
    using System;

    class AgilityPackParser : ITestee
    {
        public String Name
        {
            get { return "HTMLAgilityPack"; }
        }

        public Type Library
        {
            get { return typeof(HtmlDocument); }
        }

        public void Run(String source)
        {
            var document = new HtmlDocument();
            document.LoadHtml(source);
        }
    }
}
