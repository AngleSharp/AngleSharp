namespace AngleSharp.Performance.Html
{
    using HtmlAgilityPack;
    using System;

    class AgilityPackParser : ITestee
    {
        public String Name => "HTMLAgilityPack";

        public Type Library => typeof(HtmlDocument);

        public void Run(String source)
        {
            var document = new HtmlDocument();
            document.LoadHtml(source);
        }
    }
}
